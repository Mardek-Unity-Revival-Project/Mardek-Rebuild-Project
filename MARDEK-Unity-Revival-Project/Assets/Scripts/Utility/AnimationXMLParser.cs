///This is a utility script to parse animation data saved as an xml file, and save that as a Unity animation asset.
///This script will be updated ad hoc as I need new tags, so is probably incomplete at the moment.
///I will start by hardcoding the address of the xml file but may change this later.

///Note that this script creates the new asset in the same folder as the script, so the asset will need to be moved to a more appropriate location afterwards.
///This ensures that after the asset is moved, it is not in danger of being accidentally overwritten by rerunning this script without changing the target file name.

///This script should never run during gameplay. If you need to convert an animation, put this script on an empty object in a test scene, then remove the scene once the animation asset in generated.

///Also fair warning, I'm going to ignore error handling since this isn't a gameplay script.

///TODO:
///Fix the scaling for the translation animations
///Abstract the parser into a function, so I can parse more than one file and have one animation clip to deal with all the components
///Create a skewbox class thing to deal with skewing (will require looking up some math)
///Create a file to store paths and scale data

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;

public class AnimationXMLParser : MonoBehaviour
{
    string xmlFilePath = "Animations/Portraits/AnnunakiHeadAnim"; //Relative to Resources folder; omit the file extension
    string newAssetName = "AnnunakiHead.anim"; //Name of asset to be created; does require file extension, but not file path (asset is saved in utility script folder by default)
    
    ///These parameters are to properly scale the x and y position values.
    ///The script divides by the width and height of the asset in Adobe Animate, then multiply by the width and height in Unity.
    float animateScaleX = 103.35f;
    float animateScaleY = 141.00f;
    float unityScaleX;
    float unityScaleY;


    //Lists to hold various keyframe parameters. Note that these must be converted into arrays before creating the animation curves.
    //I'm using lists at first because arrays require that you know how large they are when you initialize them.
    List<Keyframe> xKeys;
    List<Keyframe> yKeys;
    List<Keyframe> xScaleKeys;
    List<Keyframe> yScaleKeys;
    List<Keyframe> xSkewKeys;
    List<Keyframe> ySkewKeys;
    List<Keyframe> rotKeys;
    //Strictly speaking I should have a list for zIndex as well. If it becomes necessary I'll add it, but hopefully zIndex will be constant for every shape in every animation.
    // Plus, I don't know how Unity3D deals with z-index so I'm not going to risk screwing something up right now.

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

        int flashFrameRate = 30; //Note: I could pull this from the xml if I wanted to, but I think they're all the same so I won't bother
        float frameDuration = 1 / (float)flashFrameRate;

        //Initialize lists
        xKeys = new List<Keyframe>();
        yKeys = new List<Keyframe>();
        xScaleKeys = new List<Keyframe>();
        yScaleKeys = new List<Keyframe>();
        xSkewKeys = new List<Keyframe>();
        ySkewKeys = new List<Keyframe>();
        rotKeys = new List<Keyframe>();

        //Get and print nodes
        XmlNodeList keyframes = doc.GetElementsByTagName("Keyframe");
        foreach(XmlNode k in keyframes) {
            //Debug.Log(k.Attributes["index"].Value);
            float keyframeTime = float.Parse(k.Attributes["index"].Value)*frameDuration;
            if(k.Attributes["x"] != null) xKeys.Add(new Keyframe(keyframeTime, float.Parse(k.Attributes["x"].Value)));
            if(k.Attributes["y"] != null) yKeys.Add(new Keyframe(keyframeTime, float.Parse(k.Attributes["y"].Value)));
            if(k.Attributes["scaleX"] != null) xScaleKeys.Add(new Keyframe(keyframeTime, float.Parse(k.Attributes["scaleX"].Value)));
            if(k.Attributes["scaleY"] != null) yScaleKeys.Add(new Keyframe(keyframeTime, float.Parse(k.Attributes["scaleY"].Value)));
            //if(k.Attributes["skewX"] != null) xSkewKeys.Add(new Keyframe(keyframeTime, float.Parse(k.Attributes["skewX"].Value)));
            //if(k.Attributes["skewY"] != null) ySkewKeys.Add(new Keyframe(keyframeTime, float.Parse(k.Attributes["skewY"].Value)));
            if(k.Attributes["rotation"] != null) rotKeys.Add(new Keyframe(keyframeTime, float.Parse(k.Attributes["rotation"].Value)));
        }

        //Declare and initialiaze the animation clip
        AnimationClip newClip = new AnimationClip();

        ///TODO: fix scaling on everything, especially x and y pos; make certain rotation is working in the correct direction (clockwise vs counterclockwise)

        //Create animation curves for each parameter, and add animation curves to the animation asset
        if(xKeys.Count > 0) newClip.SetCurve("test/Eye", typeof(Transform), "localPosition.x", new AnimationCurve(xKeys.ToArray()));
        if(yKeys.Count > 0) newClip.SetCurve("", typeof(Transform), "localPosition.y", new AnimationCurve(yKeys.ToArray()));
        if(xScaleKeys.Count > 0) newClip.SetCurve("", typeof(Transform), "localScale.x", new AnimationCurve(xScaleKeys.ToArray()));
        if(yScaleKeys.Count > 0) newClip.SetCurve("", typeof(Transform), "localScale.y", new AnimationCurve(yScaleKeys.ToArray()));
        if(rotKeys.Count > 0) newClip.SetCurve("", typeof(Transform), "localEulerAngles.z", new AnimationCurve(rotKeys.ToArray())); ///NOTE: do not use localRotation! That is in quaternions, which is a completely different method of recording rotation
        ///TODO: figure out how to include the skewX and skewY animations, since that isn't part of the Transform thing
        ///Apparently it is possible to recreate a skew with rotations and scales, so maybe I could have this create a separate skew animation that would apply to a some skew container?
        ///Once I work out how to animate a skew, I can use "SkewBox" as the first parameter on SetCurve to animate the skew properties of the skew box
        ///Note: to animate children of children, use "childOfMain/childOfChild"


        //Save the animation asset to the current folder
        string newFilePath = "Assets/Scripts/Utility/" + newAssetName;
        Debug.Log(newFilePath);
        AssetDatabase.CreateAsset(newClip, newFilePath);
    }

    /**
    // Update is called once per frame
    void Update()
    {
        
    }
    **/
}
