using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Vlindos.Common.Extensions.IEnumerable;

namespace Framework.Web.Assets
{
    public interface IAssetsRenderWarehouse
    {
        bool AcquireAssetsGroup(string baseUrl);
        void AddBundle(string baseUrl, AssetBundle bundle, AssetType assetType);
        IEnumerable<AssetBundle> Bundles(string baseUrl, AssetType assetType);
        void WaitRenderToComplete(string baseUrl);
        void CompleteRendering(string baseUrl);
    }

    public enum AssetType
    {
        Js,
        Css
    }

    public class AssetsRendererWarehouse : IAssetsRenderWarehouse
    {
        private readonly ConcurrentDictionary<string, Dictionary<AssetType, List<AssetBundle>>> _warehouse;
        private readonly Dictionary<string, bool> _completedRenderings; 

        public AssetsRendererWarehouse()
        {
            _warehouse = new ConcurrentDictionary<string, Dictionary<AssetType, List<AssetBundle>>>();
            _completedRenderings = new Dictionary<string, bool>();
        }

        public bool AcquireAssetsGroup(string baseUrl)
        {
            if (_warehouse.TryAdd(baseUrl, null) == false) return false;
            var localWarehouse = new Dictionary<AssetType, List<AssetBundle>>();
            ((AssetType[]) Enum.GetValues(typeof (AssetType))).ForEach(x => localWarehouse[x] = new List<AssetBundle>());
            _warehouse[baseUrl] = localWarehouse;
            _completedRenderings[baseUrl] = false;
            return true;
        }

        public void AddBundle(string baseUrl, AssetBundle bundle, AssetType assetType)
        {
            _warehouse[baseUrl][assetType].Add(bundle);
        }

        public IEnumerable<AssetBundle> Bundles(string baseUrl, AssetType assetType)
        {
            return _warehouse[baseUrl][assetType];
        }

        public void WaitRenderToComplete(string baseUrl)
        {
            const int step = 60;
            for (int i = 0; !_completedRenderings[baseUrl]; i++)
            {
                Thread.Sleep(step);
                if (i >= 1000)
                {
                    throw new InvalidProgramException(
                        string.Format("WaitRenderToComplete() timed out after {0} mileseconds. " +
                                      "Are all IAssetsRender's instances has been 'Dispose()'ed?.", i * step));
                }
            }
        }

        public void CompleteRendering(string baseUrl)
        {
            _completedRenderings[baseUrl] = true;
        }
    }
}