using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        // Files
        private static TextFile _currentTextFile;
        private static DialogueFile _currentDialogueFile;
        private static ScriptFile _currentScriptFile;
        private static ItemFile _currentItemFile;
        private static SpellFile _currentSpellFile;
        private static WizformFile _currentWizformFile;
        private static SaveFile _currentSaveFile;
        public static TextFile CurrentTextFile
        {
            get
            {
                return _currentTextFile;
            }
            set { _currentTextFile = value; }
        }
        public static DialogueFile CurrentDialogueFile
        {
            get
            {
                return _currentDialogueFile;
            }
            set { _currentDialogueFile = value; }
        }
        public static ScriptFile CurrentScriptFile
        {
            get
            {
                return _currentScriptFile;
            }
            set { _currentScriptFile = value; }
        }
        public static ItemFile CurrentItemFile
        {
            get
            {
                return _currentItemFile;
            }
            set { _currentItemFile = value; }
        }
        public static SpellFile CurrentSpellFile
        {
            get
            {
                return _currentSpellFile;
            }
            set { _currentSpellFile = value; }
        }
        public static WizformFile CurrentWizformFile
        {
            get
            {
                return _currentWizformFile;
            }
            set { _currentWizformFile = value; }
        }
        public static SaveFile CurrentSaveFile
        {
            get
            {
                return _currentSaveFile;
            }
            set { _currentSaveFile = value; }
        }
    }
}
