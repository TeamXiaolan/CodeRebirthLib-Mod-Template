using CodeRebirthLib.ContentManagement;
using CodeRebirthLib.AssetManagement;
using CodeRebirthLib;

namespace CRLib._ModTemplate.Content;

public class ExampleContentHandler : ContentHandler<ExampleContentHandler>
{
    public ExampleContentHandler(CRMod mod) : base(mod)
    {
        RegisterContent("content bundle name here", out DefaultBundle? bundle);
    }
}