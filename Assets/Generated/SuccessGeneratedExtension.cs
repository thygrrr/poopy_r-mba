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

        static readonly Success successComponent = new Success();

        public bool isSuccess {
            get { return HasComponent(ComponentIds.Success); }
            set {
                if(value != isSuccess) {
                    if(value) {
                        AddComponent(ComponentIds.Success, successComponent);
                    } else {
                        RemoveComponent(ComponentIds.Success);
                    }
                }
            }
        }

        public Entity IsSuccess(bool value) {
            isSuccess = value;
            return this;
        }
    }

    public partial class Pool {

        public Entity successEntity { get { return GetGroup(Matcher.Success).GetSingleEntity(); } }

        public bool isSuccess {
            get { return successEntity != null; }
            set {
                var entity = successEntity;
                if(value != (entity != null)) {
                    if(value) {
                        CreateEntity().isSuccess = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {

        static IMatcher _matcherSuccess;

        public static IMatcher Success {
            get {
                if(_matcherSuccess == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Success);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSuccess = matcher;
                }

                return _matcherSuccess;
            }
        }
    }
}