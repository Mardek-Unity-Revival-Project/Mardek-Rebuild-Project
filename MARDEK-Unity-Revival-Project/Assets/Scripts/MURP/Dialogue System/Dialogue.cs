using System.Collections.Generic;
using UnityEngine;
using CharacterInfo = MURP.CharacterSystem.CharacterInfo;

namespace MURP.DialogueSystem
{
    [CreateAssetMenu(menuName = "MURP/DialogueSystem/Dialogue")]
    public class Dialogue : ScriptableObject
    {        
        //[TextArea(0, 10)]
        //[SerializeField] string transcript = default;
        //[SerializeField] List<string> characterNames = new List<string>();
        //[SerializeField] List<CharacterInfo> characterInfoRefs = new List<CharacterSystem.CharacterInfo>();

        //[ContextMenu("Parse Transcript")]
        //void ParseTranscript()
        //{
        //    CharacterLines = new List<CharacterLines>();
        //    var transcriptLines = transcript.Split('\n');
        //    CharacterInfo character = null;
        //    List<string> lines = new List<string>();
        //    foreach(var line in transcriptLines)
        //    {
        //        bool isHeader = false;
        //        for (int i = 0; i < characterNames.Count; i++)
        //        {
        //            var characterName = characterNames[i];
        //            if (line.Contains(characterName) && line.Length - characterName.Length <= 1)
        //            {
        //                isHeader = true;
        //                if (character != null)
        //                {
        //                    CharacterLines.Add(new CharacterLines(character, lines));
        //                }
        //                character = characterInfoRefs[i];
        //                lines = new List<string>();
        //            }
        //        }
        //        if (isHeader == false) lines.Add(line);

        //    }
        //    CharacterLines.Add(new CharacterLines(character, lines));
        //}
        [field:SerializeField] public List<CharacterLines> CharacterLines { get; private set; }
    }
}