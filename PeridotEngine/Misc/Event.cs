using System;
using System.Collections.Generic;
using System.Text;

namespace PeridotEngine.Misc
{
    public class Event<TEventArgs>
    {
        private readonly List<WeakReference<EventHandler<TEventArgs>>> weakHandlers = [];
        private readonly List<EventHandler<TEventArgs>> handlers = [];
        private readonly Lock syncRoot = new();

        public void Invoke(object sender, TEventArgs args)
        {
            lock (syncRoot)
            {
                foreach (EventHandler<TEventArgs> handler in handlers)
                {
                    handler.Invoke(sender, args);
                }

                weakHandlers.RemoveAll(weakRef =>
                {
                    // remove dead references
                    if (!weakRef.TryGetTarget(out EventHandler<TEventArgs>? handler)) return true;

                    // invoke live references, don't remove them
                    handler.Invoke(sender, args);
                    return false;
                });
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
            lock (syncRoot)
            {
                weakHandlers.Add(new WeakReference<EventHandler<TEventArgs>>(handler));
            }
        }

        public void RemoveHandler(EventHandler<TEventArgs> handler)
        {
            lock (syncRoot)
            {
                handlers.Remove(handler);
                weakHandlers.RemoveAll(x =>
                {
                    if (x.TryGetTarget(out EventHandler<TEventArgs>? target))
                    {
                        return target == handler;
                    }
                    return true;
                });
            }
        }
    }
}
