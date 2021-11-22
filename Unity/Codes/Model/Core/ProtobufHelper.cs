﻿using System;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;
using UnityEngine;

namespace ET
{
    public static class ProtobufHelper
    {
        private const string messagePath = "Assets/Bundles/Lua/pbc/AutoGeneratedCode/OuterMessage.txt";

        private const string configPath = "Assets/Bundles/Lua/pbc/config.proto.txt";

        static ProtobufHelper()
        {
#if __CSharpLua__
            /*
            [[
                   local protoc = require "protoc"
            ]]
            */
            var textAsset = ET.LoadHelper.LoadTextAsset(messagePath);
           var  textAssetConfig = ET.LoadHelper.LoadTextAsset(configPath);
            if (textAsset != null)
            {
                string allmsg = textAsset.text + textAssetConfig.text;
                /*
                 [[
                        protoc:load(allmsg)
                 ]]
                 */
            }
#endif
        }

        public static void Init()
        {
        }

        public static object FromBytes(Type type, byte[] bytes, int index, int count)
        {
#if !__CSharpLua__
	        using (MemoryStream stream = new MemoryStream(bytes, index, count))
	        {
		        return RuntimeTypeModel.Default.Deserialize(stream, null, type);
	        }
#else
            string name = type.FullName;
            if (bytes != null)
            {
                /*
                [[
                return decodeProtobuf1(bytes, name)
                ]]
                */
            }
            return null;
#endif
        }

        public static byte[] ToBytes(object message)
        {
#if !__CSharpLua__
	        using (MemoryStream stream = new MemoryStream())
	        {
		        ProtoBuf.Serializer.Serialize(stream, message);
		        return stream.ToArray();
	        }
#else
	        byte[] bytes = null;

	        /*
	        [[
	         bytes = encodeProtobuf(message)
	        ]]
	        */
	        return bytes;
#endif
        }

        public static void ToStream(object message, MemoryStream stream)
        {
#if !__CSharpLua__
            ProtoBuf.Serializer.Serialize(stream, message);
#else
	        byte[] bytes = null;

	        /*
	        [[
	         bytes = encodeProtobuf(message)
	        ]]
	        */

	        ET.StreamHelper.WriteBytes(stream, bytes);
#endif
        }

        public static object FromStream(Type type, MemoryStream stream)
        {
#if !__CSharpLua__
	        return RuntimeTypeModel.Default.Deserialize(stream, null, type);
#else
            byte[] bytes = ET.StreamHelper.ReadBytes(stream, ET.Packet.OpcodeLength);
            string name = type.FullName;
            if (bytes != null)
            {
                /*
                [[
                return decodeProtobuf1(bytes, name)
                ]]
                */
            }

            return null;
#endif
        }
    }
}