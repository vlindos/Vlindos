namespace Framework.Web.JavaScript
{
    public interface IAssetsTransformersManager
    {
        IJsTransformer GetJsTransformer();
        ICssTransformer GetCssTransformer();
    }

    public class AssetsTransformersManager : IAssetsTransformersManager
    {
        private readonly INoTransformJsTransformer _jsTransformer;
        private readonly INoTransformCssTransformer _cssTransformer;

        public AssetsTransformersManager(
            INoTransformJsTransformer jsTransformer,
            INoTransformCssTransformer cssTransformer)
        {
            _jsTransformer = jsTransformer;
            _cssTransformer = cssTransformer;
        }

        public IJsTransformer GetJsTransformer()
        {
            return _jsTransformer;
        }

        public ICssTransformer GetCssTransformer()
        {
            return _cssTransformer;
        }
    }
}