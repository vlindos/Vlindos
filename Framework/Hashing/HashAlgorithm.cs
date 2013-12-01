using System;

/***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is HashTableHashing.MurmurHash2.
 *
 * The Initial Developer of the Original Code is
 * Davy Landman.
 * Portions created by the Initial Developer are Copyright (C) 2009
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */
namespace Vlindos.Common.Hashing
{
    public interface IHashAlgorithm
    {
        UInt32 Hash(Byte[] data);
    }


    //public class MurmurHash2Unsafe : IHashAlgorithm
    //{
    //    public UInt32 Hash(Byte[] data)
    //    {
    //        return Hash(data, 0xc58f1a7b);
    //    }
    //    const UInt32 m = 0x5bd1e995;
    //    const Int32 r = 24;

    //    public unsafe UInt32 Hash(Byte[] data, UInt32 seed)
    //    {
    //        Int32 length = data.Length;
    //        if (length == 0)
    //            return 0;
    //        UInt32 h = seed ^ (UInt32)length;
    //        Int32 remainingBytes = length & 3; // mod 4
    //        Int32 numberOfLoops = length >> 2; // div 4
    //        fixed (byte* firstByte = &(data[0]))
    //        {
    //            UInt32* realData = (UInt32*)firstByte;
    //            while (numberOfLoops != 0)
    //            {
    //                UInt32 k = *realData;
    //                k *= m;
    //                k ^= k >> r;
    //                k *= m;

    //                h *= m;
    //                h ^= k;
    //                numberOfLoops--;
    //                realData++;
    //            }
    //            switch (remainingBytes)
    //            {
    //                case 3:
    //                    h ^= (UInt16)(*realData);
    //                    h ^= ((UInt32)(*(((Byte*)(realData)) + 2))) << 16;
    //                    h *= m;
    //                    break;
    //                case 2:
    //                    h ^= (UInt16)(*realData);
    //                    h *= m;
    //                    break;
    //                case 1:
    //                    h ^= *((Byte*)realData);
    //                    h *= m;
    //                    break;
    //            }
    //        }

    //        // Do a few final mixes of the hash to ensure the last few
    //        // bytes are well-incorporated.

    //        h ^= h >> 13;
    //        h *= m;
    //        h ^= h >> 15;

    //        return h;
    //    }
    //}
    public class SuperFastHashInlineBitConverter : IHashAlgorithm
    {
        public UInt32 Hash(Byte[] dataToHash)
        {
            var dataLength = dataToHash.Length;
            if (dataLength == 0)
                return 0;
            var hash = (UInt32)dataLength;
            Int32 remainingBytes = dataLength & 3; // mod 4
            Int32 numberOfLoops = dataLength >> 2; // div 4
            Int32 currentIndex = 0;
            while (numberOfLoops > 0)
            {
                hash += (UInt16)(dataToHash[currentIndex++] | dataToHash[currentIndex++] << 8);
                UInt32 tmp = ((UInt32)(dataToHash[currentIndex++] | dataToHash[currentIndex++] << 8) << 11) ^ hash;
                hash = (hash << 16) ^ tmp;
                hash += hash >> 11;
                numberOfLoops--;
            }

            switch (remainingBytes)
            {
                case 3:
                    hash += (UInt16)(dataToHash[currentIndex++] | dataToHash[currentIndex++] << 8);
                    hash ^= hash << 16;
                    hash ^= ((UInt32)dataToHash[currentIndex]) << 18;
                    hash += hash >> 11;
                    break;
                case 2:
                    hash += (UInt16)(dataToHash[currentIndex++] | dataToHash[currentIndex] << 8);
                    hash ^= hash << 11;
                    hash += hash >> 17;
                    break;
                case 1:
                    hash += dataToHash[currentIndex];
                    hash ^= hash << 10;
                    hash += hash >> 1;
                    break;
            }

            /* Force "avalanching" of final 127 bits */
            hash ^= hash << 3;
            hash += hash >> 5;
            hash ^= hash << 4;
            hash += hash >> 17;
            hash ^= hash << 25;
            hash += hash >> 6;

            return hash;
        }
    }

}
