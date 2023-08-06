using MoreMountains.TopDownEngine;
using System.Collections.Generic;
using System.Linq;
using Debug = UnityEngine.Debug;

public class AttributeManager : TopDownMonoBehaviour
{
    private CharacterAttributes CharacterAttributes = new CharacterAttributes();
    private bool _attributesLoaded = false;

    public bool TryLoadAttributeValues(CharacterBaseClass characterBaseClass)
    {
        List<CharacterAttributes> attributes = JSONManager<CharacterAttributes>.GetJsonValues("");

        CharacterAttributes result = attributes.Where(a => a.CharacterBaseType == characterBaseClass).SingleOrDefault();
        if (result != null)
        {
            CharacterAttributes = result;
            _attributesLoaded = true;
        }
        else
        {
            Debug.LogError("Couldn't load attribute values");
            _attributesLoaded = false;
        }

        return _attributesLoaded;
    }
    public bool IsAttributesLoaded()
    { 
        return _attributesLoaded;
    }
    public CharacterAttributes GetAllCharacterAttributes()
    {
        return CharacterAttributes;
    }
}