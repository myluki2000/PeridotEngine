using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PeridotEngine.Misc
{
    public class Event<TEventArgs>
    {
        private readonly List<WeakHandler> weakHandlers = [];
        private readonly List<EventHandler<TEventArgs>> handlers = [];
        private readonly Lock syncRoot = new();

        public void Invoke(object sender, TEventArgs args)
        {
            EventHandler<TEventArgs>[] strong;
            WeakHandler[] weak;

            lock (syncRoot)
            {
                strong = handlers.ToArray();
                weak = weakHandlers.ToArray();
            }

            foreach (EventHandler<TEventArgs> handler in strong)
                handler(sender, args);


            List<WeakHandler> markedForDeletion = new(10);
            foreach (WeakHandler handler in weak)
            {
                if (handler.Target.IsAlive)
                {
                    handler.Handler(handler.Target.Target!, sender, args);
                }
                else
                {
                    markedForDeletion.Add(handler);
                }
            }

            lock (syncRoot)
            {
                weakHandlers.RemoveAll(x => markedForDeletion.Contains(x));
            }
        }

        public void AddHandler(EventHandler<TEventArgs> handler)
        {
            lock (syncRoot)
            {
                handlers.Add(handler);
            }
        }

        public void AddWeakHandler(EventHandler<TEventArgs> handler)
        {
            if (handler.Target == null)
            {
                throw new ArgumentException(
                    "Cannot use a delegate without a target as weak handler, as it would be disposed instantly or never!");
            }

            lock (syncRoot)
            {
                weakHandlers.Add(new WeakHandler(handler));
            }
        }

        public void RemoveHandler(EventHandler<TEventArgs> handler)
        {
            lock (syncRoot)
            {
                handlers.Remove(handler);
                weakHandlers.RemoveAll(x => x.Target.Target == handler.Target && x.Method == handler.Method);
            }
        }

        public bool HasAttachedHandlers => handlers.Count > 0 || weakHandlers.Count > 0;

        private class WeakHandler
        {
            public WeakHandler(EventHandler<TEventArgs> origHandler)
            {
                Target = new WeakReference(origHandler.Target);
                Method = origHandler.Method;

                // Build fast open delegate
                ParameterExpression targetParam = Expression.Parameter(typeof(object), "target");
                ParameterExpression senderParam = Expression.Parameter(typeof(object), "sender");
                ParameterExpression argsParam = Expression.Parameter(typeof(TEventArgs), "args");

                UnaryExpression castTarget = Expression.Convert(targetParam, Target.Target.GetType());
                MethodCallExpression call = Expression.Call(castTarget, Method, senderParam, argsParam);

                Handler = Expression.Lambda<Action<object, object, TEventArgs>>(
                    call, targetParam, senderParam, argsParam
                ).Compile();
            }

            public WeakReference Target { get; }
            public Action<object, object, TEventArgs> Handler { get; }
            public MethodInfo Method { get; }
        }
    }
}
