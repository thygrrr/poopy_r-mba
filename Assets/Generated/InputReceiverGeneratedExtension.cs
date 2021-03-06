//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {

    public partial class Entity {

        static readonly InputReceiver inputReceiverComponent = new InputReceiver();

        public bool isInputReceiver {
            get { return HasComponent(ComponentIds.InputReceiver); }
            set {
                if(value != isInputReceiver) {
                    if(value) {
                        AddComponent(ComponentIds.InputReceiver, inputReceiverComponent);
                    } else {
                        RemoveComponent(ComponentIds.InputReceiver);
                    }
                }
            }
        }

        public Entity IsInputReceiver(bool value) {
            isInputReceiver = value;
            return this;
        }
    }

    public partial class Matcher {

        static IMatcher _matcherInputReceiver;

        public static IMatcher InputReceiver {
            get {
                if(_matcherInputReceiver == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputReceiver);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputReceiver = matcher;
                }

                return _matcherInputReceiver;
            }
        }
    }
}
