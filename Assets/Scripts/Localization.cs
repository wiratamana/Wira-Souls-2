using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Localization : MonoBehaviour
    {
        public TextAsset csv;
        private Dictionary<string, LocalizedText> _localized;
        public Dictionary<string, LocalizedText> localized
        {
            get
            {
                if (_localized == null)
                {
                    _localized = new Dictionary<string, LocalizedText>();

                    _localized.Clear();

                    var text = csv.text.Replace(System.Environment.NewLine, "|");
                    var splittedText = text.Split('|');

                    for (int i = 1; i < splittedText.Length; i++)
                    {
                        try
                        {
                            var splittedTextByComma = splittedText[i].Split(',');
                            _localized.Add(splittedTextByComma[0], new LocalizedText(splittedTextByComma[1], splittedTextByComma[2], splittedTextByComma[3]));
                        }
                        catch
                        { continue; }
                    }
                }

                return _localized;
            }
        }

        public enum Language { English, Indonesia, Japanese }
        public Language gameLanguage;

        public static string GetText(string key)
        {
            switch (instance.gameLanguage)
            {
                case Language.English:
                    return instance.localized[key].english;
                case Language.Indonesia:
                    return instance.localized[key].indonesia;
                case Language.Japanese:
                    return instance.localized[key].japanese;
            }

            return string.Empty;
        }

        public static Localization instance { private set; get; }

        private void Awake()
        {
            instance = this;
        }

        public struct LocalizedText
        {
            public string english;
            public string japanese;
            public string indonesia;

            public LocalizedText(string english, string japanese, string indonesia)
            {
                this.english = english;
                this.japanese = japanese;
                this.indonesia = indonesia;
            }
        }
    }
}
