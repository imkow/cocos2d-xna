using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using cocos2d.Compression.Zlib;
#if WINDOWS_PHONE
using WP7Contrib.Communications.Compression;
#else
using System.IO.Compression;
#endif

namespace cocos2d
{
    /// <summary>
    /// CCTMXMapInfo contains the information about the map like:
    ///- Map orientation (hexagonal, isometric or orthogonal)
    ///- Tile size
    ///- Map size
    ///
    ///	And it also contains:
    ///- Layers (an array of TMXLayerInfo objects)
    ///- Tilesets (an array of TMXTilesetInfo objects)
    ///- ObjectGroups (an array of TMXObjectGroupInfo objects)
    ///
    ///This information is obtained from the TMX file.
    /// </summary>
    public class CCTMXMapInfo : CCObject, ICCSAXDelegator
    {
        #region properties

        protected bool m_bStoringCharacters;
        protected int m_nLayerAttribs;
        protected int m_nOrientation;
        protected int m_nParentElement;
        protected List<CCTMXLayerInfo> m_pLayers;
        protected List<CCTMXObjectGroup> m_pObjectGroups;
        protected Dictionary<string, string> m_pProperties;
        protected Dictionary<uint, Dictionary<string, string>> m_pTileProperties;
        protected List<CCTMXTilesetInfo> m_pTilesets;
        protected byte[] m_sCurrentString;
        private string m_sResources;
        protected string m_sTMXFileName;

        protected CCSize m_tMapSize;

        protected CCSize m_tTileSize;
        protected uint m_uParentGID;

        /// <summary>
        ///  map orientation
        /// </summary>
        public int Orientation
        {
            get { return m_nOrientation; }
            set { m_nOrientation = value; }
        }

        /// <summary>
        /// map width & height
        /// </summary>
        public CCSize MapSize
        {
            get { return m_tMapSize; }
            set { m_tMapSize = value; }
        }

        /// <summary>
        /// tiles width & height
        /// </summary>
        public CCSize TileSize
        {
            get { return m_tTileSize; }
            set { m_tTileSize = value; }
        }

        /// <summary>
        /// Layers
        /// </summary>
        public virtual List<CCTMXLayerInfo> Layers
        {
            get { return m_pLayers; }
            set { m_pLayers = value; }
        }

        /// <summary>
        /// tilesets
        /// </summary>
        public virtual List<CCTMXTilesetInfo> Tilesets
        {
            get { return m_pTilesets; }
            set { m_pTilesets = value; }
        }

        /// <summary>
        /// ObjectGroups
        /// </summary>
        public virtual List<CCTMXObjectGroup> ObjectGroups
        {
            get { return m_pObjectGroups; }
            set { m_pObjectGroups = value; }
        }

        /// <summary>
        /// parent element
        /// </summary>
        public int ParentElement
        {
            get { return m_nParentElement; }
            set { m_nParentElement = value; }
        }

        /// <summary>
        /// parent GID
        /// </summary>
        public uint ParentGID
        {
            get { return m_uParentGID; }
            set { m_uParentGID = value; }
        }

        /// <summary>
        /// layer attribs
        /// </summary>
        public int LayerAttribs
        {
            get { return m_nLayerAttribs; }
            set { m_nLayerAttribs = value; }
        }

        /// <summary>
        /// is stroing characters?
        /// </summary>
        public bool StoringCharacters
        {
            get { return m_bStoringCharacters; }
            set { m_bStoringCharacters = value; }
        }

        /// <summary>
        /// properties
        /// </summary>
        public Dictionary<string, string> Properties
        {
            get { return m_pProperties; }
            set { m_pProperties = value; }
        }

        /// <summary>
        /// ! tmx filename
        /// </summary>
        public string TMXFileName
        {
            get { return m_sTMXFileName; }
            set { m_sTMXFileName = value; }
        }

        /// <summary>
        /// ! current string
        /// </summary>
        public byte[] CurrentString
        {
            get { return m_sCurrentString; }
            set { m_sCurrentString = value; }
        }

        /// <summary>
        /// ! tile properties
        /// </summary>
        public Dictionary<uint, Dictionary<string, string>> TileProperties
        {
            get { return m_pTileProperties; }
            set { m_pTileProperties = value; }
        }

        #endregion

        #region ICCSAXDelegator Members

        public void StartElement(object ctx, string name, string[] atts)
        {
            CCTMXMapInfo pTMXMapInfo = this;
            string elementName = name;
            var attributeDict = new Dictionary<string, string>();

            if (atts != null && atts[0] != null)
            {
                for (int i = 0; i + 1 < atts.Length; i += 2)
                {
                    string key = atts[i];
                    string value = atts[i + 1];
                    attributeDict.Add(key, value);
                }
            }
            if (elementName == "map")
            {
                string version = attributeDict["version"];
                if (version != "1.0")
                {
                    CCLog.Log("cocos2d: TMXFormat: Unsupported TMX version: {0}", version);
                }
                string orientationStr = attributeDict["orientation"];
                if (orientationStr == "orthogonal")
                    pTMXMapInfo.Orientation = (int) (CCTMXOrientatio.CCTMXOrientationOrtho);
                else if (orientationStr == "isometric")
                    pTMXMapInfo.Orientation = (int) (CCTMXOrientatio.CCTMXOrientationIso);
                else if (orientationStr == "hexagonal")
                    pTMXMapInfo.Orientation = (int) (CCTMXOrientatio.CCTMXOrientationHex);
                else
                    CCLog.Log("cocos2d: TMXFomat: Unsupported orientation: {0}", pTMXMapInfo.Orientation);

                CCSize sMapSize;
                sMapSize.width = ccUtils.ccParseFloat(attributeDict["width"]);
                sMapSize.height = ccUtils.ccParseFloat(attributeDict["height"]);
                pTMXMapInfo.MapSize = sMapSize;

                CCSize sTileSize;
                sTileSize.width = ccUtils.ccParseFloat(attributeDict["tilewidth"]);
                sTileSize.height = ccUtils.ccParseFloat(attributeDict["tileheight"]);
                pTMXMapInfo.TileSize = sTileSize;

                // The parent element is now "map"
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyMap;
            }
            else if (elementName == "tileset")
            {
                // If this is an external tileset then start parsing that

                if (attributeDict.Keys.Contains("source"))
                {
                    string externalTilesetFilename = attributeDict["source"];

                    externalTilesetFilename = CCFileUtils.fullPathFromRelativeFile(externalTilesetFilename, pTMXMapInfo.TMXFileName);
                    pTMXMapInfo.ParseXmlFile(externalTilesetFilename);
                }
                else
                {
                    var tileset = new CCTMXTilesetInfo();

                    tileset.m_sName = attributeDict["name"];
                    tileset.m_uFirstGid = uint.Parse(attributeDict["firstgid"]);

                    if (attributeDict.Keys.Contains("spacing"))
                        tileset.m_uSpacing = int.Parse(attributeDict["spacing"]);

                    if (attributeDict.Keys.Contains("margin"))
                        tileset.m_uMargin = int.Parse(attributeDict["margin"]);

                    CCSize s;
                    s.width = ccUtils.ccParseFloat(attributeDict["tilewidth"]);
                    s.height = ccUtils.ccParseFloat(attributeDict["tileheight"]);
                    tileset.m_tTileSize = s;

                    pTMXMapInfo.Tilesets.Add(tileset);
                }
            }
            else if (elementName == "tile")
            {
                CCTMXTilesetInfo info = pTMXMapInfo.Tilesets.LastOrDefault();
                var dict = new Dictionary<string, string>();
                pTMXMapInfo.ParentGID = (info.m_uFirstGid + uint.Parse(attributeDict["id"]));
                pTMXMapInfo.TileProperties.Add(pTMXMapInfo.ParentGID, dict);

                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyTile;
            }
            else if (elementName == "layer")
            {
                var layer = new CCTMXLayerInfo();
                layer.m_sName = attributeDict["name"];

                CCSize s;
                s.width = ccUtils.ccParseFloat(attributeDict["width"]);
                s.height = ccUtils.ccParseFloat(attributeDict["height"]);
                layer.m_tLayerSize = s;

                layer.m_pTiles = new uint[(int) s.width * (int) s.height];

                if (attributeDict.Keys.Contains("visible"))
                {
                    string visible = attributeDict["visible"];
                    layer.m_bVisible = !(visible == "0");
                }
                else
                {
                    layer.m_bVisible = true;
                }

                if (attributeDict.Keys.Contains("opacity"))
                {
                    string opacity = attributeDict["opacity"];
                    layer.m_cOpacity = (byte) (255 * ccUtils.ccParseFloat(opacity));
                }
                else
                {
                    layer.m_cOpacity = 255;
                }

                float x = attributeDict.Keys.Contains("x") ? ccUtils.ccParseFloat(attributeDict["x"]) : 0;
                float y = attributeDict.Keys.Contains("y") ? ccUtils.ccParseFloat(attributeDict["y"]) : 0;
                layer.m_tOffset = new CCPoint(x, y);

                pTMXMapInfo.Layers.Add(layer);

                // The parent element is now "layer"
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyLayer;
            }
            else if (elementName == "objectgroup")
            {
                var objectGroup = new CCTMXObjectGroup();
                objectGroup.GroupName = attributeDict["name"];

                CCPoint positionOffset = CCPoint.Zero;
                if (attributeDict.ContainsKey("x"))
                    positionOffset.x = ccUtils.ccParseFloat(attributeDict["x"]) * pTMXMapInfo.TileSize.width;
                if (attributeDict.ContainsKey("y"))
                    positionOffset.y = ccUtils.ccParseFloat(attributeDict["y"]) * pTMXMapInfo.TileSize.height;
                objectGroup.PositionOffset = positionOffset;

                pTMXMapInfo.ObjectGroups.Add(objectGroup);

                // The parent element is now "objectgroup"
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyObjectGroup;
            }
            else if (elementName == "image")
            {
                CCTMXTilesetInfo tileset = pTMXMapInfo.Tilesets.LastOrDefault();

                // build full path
                string imagename = attributeDict["source"];
                tileset.m_sSourceImage = CCFileUtils.fullPathFromRelativeFile(imagename, pTMXMapInfo.TMXFileName);
            }
            else if (elementName == "data")
            {
                string encoding = attributeDict.ContainsKey("encoding") ? attributeDict["encoding"] : "";
                string compression = attributeDict.ContainsKey("compression") ? attributeDict["compression"] : "";

                if (encoding == "base64")
                {
                    int layerAttribs = pTMXMapInfo.LayerAttribs;
                    pTMXMapInfo.LayerAttribs = layerAttribs | (int) TMXLayerAttrib.TMXLayerAttribBase64;
                    pTMXMapInfo.StoringCharacters = true;

                    if (compression == "gzip")
                    {
                        layerAttribs = pTMXMapInfo.LayerAttribs;
                        pTMXMapInfo.LayerAttribs = layerAttribs | (int) TMXLayerAttrib.TMXLayerAttribGzip;
                    }
                    else if (compression == "zlib")
                    {
                        layerAttribs = pTMXMapInfo.LayerAttribs;
                        pTMXMapInfo.LayerAttribs = layerAttribs | (int) TMXLayerAttrib.TMXLayerAttribZlib;
                    }
                    Debug.Assert(compression == "" || compression == "gzip" || compression == "zlib", "TMX: unsupported compression method");
                }
                Debug.Assert(pTMXMapInfo.LayerAttribs != (int) TMXLayerAttrib.TMXLayerAttribNone,
                             "TMX tile map: Only base64 and/or gzip/zlib maps are supported");
            }
            else if (elementName == "object")
            {
                CCTMXObjectGroup objectGroup = pTMXMapInfo.ObjectGroups.LastOrDefault();

                // The value for "type" was blank or not a valid class name
                // Create an instance of TMXObjectInfo to store the object and its properties
                var dict = new Dictionary<string, string>();

                var pArray = new[] {"name", "type", "width", "height", "gid"};

                for (int i = 0; i < pArray.Length; i++)
                {
                    string key = pArray[i];
                    if (attributeDict.ContainsKey(key))
                    {
                        dict.Add(key, attributeDict[key]);
                    }
                }

                // But X and Y since they need special treatment
                // X

                int x = int.Parse(attributeDict["x"]) + (int) objectGroup.PositionOffset.x;
                dict.Add("x", x.ToString());

                int y = int.Parse(attributeDict["y"]) + (int) objectGroup.PositionOffset.y;
                // Correct y position. (Tiled uses Flipped, cocos2d uses Standard)
                y = (int) (pTMXMapInfo.MapSize.height * pTMXMapInfo.TileSize.height) - y -
                    (attributeDict.ContainsKey("height") ? int.Parse(attributeDict["height"]) : 0);
                dict.Add("y", y.ToString());

                // Add the object to the objectGroup
                objectGroup.Objects.Add(dict);

                // The parent element is now "object"
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyObject;
            }
            else if (elementName == "property")
            {
                if (pTMXMapInfo.ParentElement == (int) TMXProperty.TMXPropertyNone)
                {
                    CCLog.Log("TMX tile map: Parent element is unsupported. Cannot add property named '{0}' with value '{1}'",
                              attributeDict["name"], attributeDict["value"]);
                }
                else if (pTMXMapInfo.ParentElement == (int) TMXProperty.TMXPropertyMap)
                {
                    // The parent element is the map
                    string value = attributeDict["value"];
                    string key = attributeDict["name"];
                    pTMXMapInfo.Properties.Add(key, value);
                }
                else if (pTMXMapInfo.ParentElement == (int) TMXProperty.TMXPropertyLayer)
                {
                    // The parent element is the last layer
                    CCTMXLayerInfo layer = pTMXMapInfo.Layers.LastOrDefault();
                    string value = attributeDict["value"];
                    string key = attributeDict["name"];
                    // Add the property to the layer
                    layer.Properties.Add(key, value);
                }
                else if (pTMXMapInfo.ParentElement == (int) TMXProperty.TMXPropertyObjectGroup)
                {
                    // The parent element is the last object group
                    CCTMXObjectGroup objectGroup = pTMXMapInfo.ObjectGroups.LastOrDefault();
                    string value = attributeDict["value"];
                    string key = attributeDict["name"];
                    objectGroup.Properties.Add(key, value);
                }
                else if (pTMXMapInfo.ParentElement == (int) TMXProperty.TMXPropertyObject)
                {
                    // The parent element is the last object
                    CCTMXObjectGroup objectGroup = pTMXMapInfo.ObjectGroups.LastOrDefault();
                    Dictionary<string, string> dict = objectGroup.Objects.LastOrDefault();

                    string propertyName = attributeDict["name"];
                    string propertyValue = attributeDict["value"];
                    dict.Add(propertyName, propertyValue);
                }
                else if (pTMXMapInfo.ParentElement == (int) TMXProperty.TMXPropertyTile)
                {
                    Dictionary<string, string> dict = pTMXMapInfo.TileProperties[pTMXMapInfo.ParentGID];

                    string propertyName = attributeDict["name"];
                    string propertyValue = attributeDict["value"];
                    dict.Add(propertyName, propertyValue);
                }
            }
            else if (elementName == "polygon")
            {
                // find parent object's dict and add polygon-points to it
                // CCTMXObjectGroup* objectGroup = (CCTMXObjectGroup*)m_pObjectGroups->lastObject();
                // CCDictionary* dict = (CCDictionary*)objectGroup->getObjects()->lastObject();
                // TODO: dict->setObject(attributeDict objectForKey:@"points"] forKey:@"polygonPoints"];
            }
            else if (elementName == "polyline")
            {
                // find parent object's dict and add polyline-points to it
                // CCTMXObjectGroup* objectGroup = (CCTMXObjectGroup*)m_pObjectGroups->lastObject();
                // CCDictionary* dict = (CCDictionary*)objectGroup->getObjects()->lastObject();
                // TODO: dict->setObject:[attributeDict objectForKey:@"points"] forKey:@"polylinePoints"];
            }

            if (attributeDict != null)
            {
                attributeDict = null;
            }
        }

        public void EndElement(object ctx, string elementName)
        {
            CCTMXMapInfo pTMXMapInfo = this;
            byte[] encoded = null;

            if (elementName == "data" && (pTMXMapInfo.LayerAttribs & (int) TMXLayerAttrib.TMXLayerAttribBase64) != 0)
            {
                pTMXMapInfo.StoringCharacters = false;
                CCTMXLayerInfo layer = pTMXMapInfo.Layers.LastOrDefault();
                if ((pTMXMapInfo.LayerAttribs & ((int) (TMXLayerAttrib.TMXLayerAttribGzip) | (int) TMXLayerAttrib.TMXLayerAttribZlib)) != 0)
                {
                    //gzip compress
                    if ((pTMXMapInfo.LayerAttribs & (int) TMXLayerAttrib.TMXLayerAttribGzip) != 0)
                    {
#if WINDOWS_PHONE 
                        GZipStream inGZipStream = new GZipStream(new MemoryStream(pTMXMapInfo.CurrentString));
#else
                        var inGZipStream = new GZipStream(new MemoryStream(pTMXMapInfo.CurrentString), CompressionMode.Decompress);
#endif

                        var outMemoryStream = new MemoryStream();

                        var buffer = new byte[1024];
                        while (true)
                        {
                            int bytesRead = inGZipStream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                                break;
                            outMemoryStream.Write(buffer, 0, bytesRead);
                        }

                        encoded = outMemoryStream.ToArray();
                    }

                    //zlib
                    if ((pTMXMapInfo.LayerAttribs & (int) TMXLayerAttrib.TMXLayerAttribZlib) != 0)
                    {
                        var inZInputStream = new ZInputStream(new MemoryStream(pTMXMapInfo.CurrentString));

                        var outMemoryStream = new MemoryStream();

                        var buffer = new byte[1024];
                        while (true)
                        {
                            int bytesRead = inZInputStream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                                break;
                            outMemoryStream.Write(buffer, 0, bytesRead);
                        }

                        encoded = outMemoryStream.ToArray();
                    }
                }
                else
                {
                    encoded = pTMXMapInfo.CurrentString;
                }

                for (int i = 0; i < layer.m_pTiles.Length; i++)
                {
                    int i4 = i * 4;
                    var gid = (uint) (
                                         encoded[i4] |
                                         encoded[i4 + 1] << 8 |
                                         encoded[i4 + 2] << 16 |
                                         encoded[i4 + 3] << 24);

                    layer.m_pTiles[i] = gid;
                }

                pTMXMapInfo.CurrentString = null;
            }
            else if (elementName == "map")
            {
                // The map element has ended
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyNone;
            }
            else if (elementName == "layer")
            {
                // The layer element has ended
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyNone;
            }
            else if (elementName == "objectgroup")
            {
                // The objectgroup element has ended
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyNone;
            }
            else if (elementName == "object")
            {
                // The object element has ended
                pTMXMapInfo.ParentElement = (int) TMXProperty.TMXPropertyNone;
            }
        }

        public void TextHandler(object ctx, byte[] ch, int len)
        {
            CCTMXMapInfo pTMXMapInfo = this;

            if (pTMXMapInfo.StoringCharacters)
            {
                pTMXMapInfo.CurrentString = ch;
            }
        }

        #endregion

        /// <summary>
        /// creates a TMX Format with a tmx file
        /// </summary>
        public static CCTMXMapInfo Create(string tmxFile)
        {
            var pRet = new CCTMXMapInfo();
            pRet.InitWithTmxFile(tmxFile);
            return pRet;
        }

        private void InternalInit(string tmxFileName, string resourcePath)
        {
            m_pTilesets = new List<CCTMXTilesetInfo>();
            m_pLayers = new List<CCTMXLayerInfo>();

            if (tmxFileName != null)
            {
                m_sTMXFileName = CCFileUtils.fullPathFromRelativePath(tmxFileName);
            }

            if (resourcePath != null)
            {
                m_sResources = resourcePath;
            }

            m_pObjectGroups = new List<CCTMXObjectGroup>(4);

            m_pProperties = new Dictionary<string, string>();
            m_pTileProperties = new Dictionary<uint, Dictionary<string, string>>();

            // tmp vars
            m_sCurrentString = null;
            m_bStoringCharacters = false;
            m_nLayerAttribs = (int) TMXLayerAttrib.TMXLayerAttribNone;
            m_nParentElement = (int) TMXProperty.TMXPropertyNone;
        }

        /// <summary>
        /// initializes a TMX format witha  tmx file
        /// </summary>
        public bool InitWithTmxFile(string tmxFile)
        {
            InternalInit(tmxFile, null);
            return ParseXmlFile(m_sTMXFileName);
        }

        public bool InitWithXml(string tmxString, string resourcePath)
        {
            InternalInit(null, resourcePath);
            return ParseXmlString(tmxString);
        }

        public bool ParseXmlString(string data)
        {
            var parser = new CCSAXParser();

            if (false == parser.init("UTF-8"))
            {
                return false;
            }

            parser.setDelegator(this);

            return parser.parse(data, data.Length);
        }

        /// <summary>
        /// initalises parsing of an XML file, either a tmx (Map) file or tsx (Tileset) file
        /// </summary>
        public bool ParseXmlFile(string xmlFilename)
        {
            var parser = new CCSAXParser();

            if (false == parser.init("UTF-8"))
            {
                return false;
            }

            parser.setDelegator(this);

            return parser.parse(xmlFilename);
        }

        // the XML parser calls here with all the elements
    }
}