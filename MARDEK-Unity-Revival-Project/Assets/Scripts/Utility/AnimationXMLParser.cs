///This is a utility script to parse animation data saved as an xml file, and save that as a Unity animation asset.
///This script will be updated ad hoc as I need new tags, so is probably incomplete at the moment.
///I will start by hardcoding the address of the xml file but may change this later.

///Note that this script creates the new asset in the same folder as the script, so the asset will need to be moved to a more appropriate location afterwards.
///This ensures that after the asset is moved, it is not in danger of being accidentally overwritten by rerunning this script without changing the target file name.

///This script should never run during gameplay. If you need to convert an animation, put this script on an empty object in a test scene, then remove the scene once the animation asset in generated.

///Also fair warning, I'm going to ignore error handling since this isn't a gameplay script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class AnimationXMLParser : MonoBehaviour
{
    string xmlFilePath = "Animations/Portraits/AnnunakiHeadAnim"; //Relative to Resources folder; omit the file extension
    string newAssetName = "AnnunakiHead.anim"; //Name of asset to be created; does require file extension, but not file path (asset is saved in utility script folder by default)

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Venator's script is running");

        //Most of the xml code is adapted/corrected from https://forum.unity.com/threads/reading-xml-resource-file.108421/

        //Load the animation xml as text
        TextAsset xmlData = new TextAsset();
        xmlData = Resources.Load<TextAsset>(xmlFilePath);

        //Load the text as xml doc
        XmlDocument doc = new XmlDocument();
        doc.PreserveWhitespace = true;
        doc.LoadXml(xmlData.text);

        //Get and print nodes
        XmlNodeList keyframes = doc.GetElementsByTagName("Keyframe");
        foreach(XmlNode keyframe in keyframes) {
            Debug.Log(keyframe.Attributes["index"].Value);
            //This is where I deal with the animation things
        }

        string newFilePath = "Assets/Scripts/Utility/" + newAssetName;
        Debug.Log(newFilePath);
        //AssetDatabase.CreateAsset(newAnim, newFilePath);
    }

    /**
    // Update is called once per frame
    void Update()
    {
        
    }
    **/
}
