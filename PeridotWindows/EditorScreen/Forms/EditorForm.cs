using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics;
using PeridotEngine.Misc;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.EditorScreen.Controls;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class EditorForm : Form
    {
        public PeridotEngineControl Engine { get; }
        public ToolboxControl Toolbox { get; }
        public EntityPropertiesControl EntityPropertiesPanel { get; }
        public ResourcesControl ResourcesControl { get; }
        public SceneControl SceneControl { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditorScreen Editor
        {
            get => editor;
            set
            {
                EditorScreen oldEditor = editor;
                editor = value;
                if (!Engine.IsInitialized)
                {
                    throw new Exception("Cannot set editor before engine is initialized!");
                }
                ScreenManager.CurrentScreen = editor;
                EditorScreenChanged.Invoke(this, new EditorScreenChangedEventArgs(oldEditor, editor));
            }
        }

        public Event<EditorScreenChangedEventArgs> EditorScreenChanged { get; } = new();

        private EditorScreen editor;


        public EditorForm()
        {
            InitializeComponent();

            editor = new EditorScreen(this, new Scene3D());

            Toolbox = new ToolboxControl(this)
            {
                Dock = DockStyle.Top,
            };
            Controls.Add(Toolbox);

            Engine = new PeridotEngineControl()
            {
                Dock = DockStyle.Fill,
                GraphicsProfile = GraphicsProfile.HiDef,
            };
            pnlEngineContainer.Controls.Add(Engine);

            EntityPropertiesPanel = new EntityPropertiesControl(this)
            {
                Dock = DockStyle.Fill,
            };
            pnlEntityPropertiesContainer.Controls.Add(EntityPropertiesPanel);

            ResourcesControl = new ResourcesControl(this)
            {
                Dock = DockStyle.Fill,
            };
            pnlResourcesContainer.Controls.Add(ResourcesControl);

            SceneControl = new SceneControl(this)
            {
                Dock = DockStyle.Fill,
            };
            pnlSceneContainer.Controls.Add(SceneControl);

            ScreenManager.CurrentScreen = editor;
            EditorScreenChanged.Invoke(this, new EditorScreenChangedEventArgs(null, editor));
        }

        public EditorForm(EditorScreen editor) : this()
        {
            Editor = editor;
        }
    }

    public class EditorScreenChangedEventArgs(EditorScreen old, EditorScreen @new) : EventArgs
    {
        public EditorScreen? Old { get; } = old;
        public EditorScreen New { get; } = @new;
    }
}
