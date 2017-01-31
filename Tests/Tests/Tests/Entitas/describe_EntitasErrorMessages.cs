// TODO

//using System;
//using Entitas;
//using Entitas.Blueprints;
//using NSpec;

//class describe_EntitasErrorMessages : nspec {

//    static void printErrorMessage(Action action) {
//        try {
//            action();
//        } catch(Exception exception) {
//            Console.ForegroundColor = ConsoleColor.DarkCyan;
//            Console.WriteLine("================================================================================");
//            Console.WriteLine("Exception preview for: " + exception.GetType());
//            Console.WriteLine("--------------------------------------------------------------------------------");
//            Console.WriteLine(exception.Message);
//            Console.WriteLine("================================================================================");
//            Console.ResetColor();
//        }
//    }

//    void when_throwing() {

//        before = () => {
//            var componentNames = new [] { "Health", "Position", "View" };
//            var contextInfo = new ContextInfo("My Context", componentNames, null);
//            _context = new TestContext(componentNames.Length, 42, contextInfo);
//            _entity = createEntity();
//        };

//        context["Entity"] = () => {

//            context["when not enabled"] = () => {

//                before = () => {
//                    _context.DestroyEntity(_entity);
//                };

//                it["add a component"] = () => printErrorMessage(() => _entity.AddComponentA());
//                it["remove a component"] = () => printErrorMessage(() => _entity.RemoveComponentA());
//                it["replace a component"] = () => printErrorMessage(() => _entity.ReplaceComponentA(Component.A));
//            };

//            context["when enabled"] = () => {

//                it["add a component twice"] = () => printErrorMessage(() => {
//                    _entity.AddComponentA();
//                    _entity.AddComponentA();
//                });

//                it["remove a component that doesn't exist"] = () => printErrorMessage(() => {
//                    _entity.RemoveComponentA();
//                });

//                it["get a component that doesn't exist"] = () => printErrorMessage(() => {
//                    _entity.GetComponentA();
//                });

//                it["retain an entity twice"] = () => printErrorMessage(() => {
//                    var owner = new object();
//                    _entity.Retain(owner);
//                    _entity.Retain(owner);
//                });

//                it["release an entity with wrong owner"] = () => printErrorMessage(() => {
//                    var owner = new object();
//                    _entity.Release(owner);
//                });
//            };
//        };

//        context["Group"] = () => {

//            it["get single entity when multiple exist"] = () => printErrorMessage(() => {
//                createEntityA();
//                createEntityA();
//                var matcher = createMatcherA();
//                matcher.componentNames = _context.contextInfo.componentNames;
//                var group = _context.GetGroup(matcher);
//                group.GetSingleEntity();
//            });
//        };

//        context["Collector<TestEntity>"] = () => {

//            it["unbalanced goups"] = () => printErrorMessage(() => {
//                var g1 = new Group<TestEntity>(Matcher<TestEntity>.AllOf(CID.ComponentA));
//                var g2 = new Group<TestEntity>(Matcher<TestEntity>.AllOf(CID.ComponentB));
//                var e1 = GroupEvent.Added;

//                new Collector<TestEntity>(new [] { g1, g2 }, new [] { e1 });
//            });
//        };

//        context["Context"] = () => {

//            it["wrong ContextInfo componentNames count"] = () => printErrorMessage(() => {
//                var componentNames = new [] { "Health", "Position", "View" };
//                var contextInfo = new ContextInfo("My Context", componentNames, null);
//                new TestContext(1, 0, contextInfo);
//            });

//            it["destroy entity which is not in context"] = () => printErrorMessage(() => {
//                _context.DestroyEntity(new TestEntity());
//            });

//            it["destroy retained entities"] = () => printErrorMessage(() => {
//                createEntity().Retain(this);
//                _context.DestroyAllEntities();
//            });

//            it["releases entity before destroy"] = () => printErrorMessage(() => {
//                _entity.Release(_context);
//            });

//            it["unknown entityIndex"] = () => printErrorMessage(() => {
//                _context.GetEntityIndex("unknown");
//            });

//            it["duplicate entityIndex"] = () => printErrorMessage(() => {
//                var index = new PrimaryEntityIndex<TestEntity, string>(getGroupA(), null);
//                _context.AddEntityIndex("duplicate", index);
//                _context.AddEntityIndex("duplicate", index);
//            });
//        };

//        context["CollectionExtension"] = () => {

//            it["get single entity when more than one exist"] = () => printErrorMessage(() => {
//                new IEntity[2].SingleEntity();
//            });
//        };

//        context["ComponentBlueprint"] = () => {

//            it["type doesn't implement IComponent"] = () => printErrorMessage(() => {
//                var componentBlueprint = new ComponentBlueprint();
//                componentBlueprint.fullTypeName = "string";
//                componentBlueprint.CreateComponent(_entity);
//            });

//            it["type doesn't exist"] = () => printErrorMessage(() => {
//                var componentBlueprint = new ComponentBlueprint();
//                componentBlueprint.fullTypeName = "UnknownType";
//                componentBlueprint.CreateComponent(_entity);
//            });

//            it["invalid field name"] = () => printErrorMessage(() => {
//                var componentBlueprint = new ComponentBlueprint();
//                componentBlueprint.index = 0;
//                componentBlueprint.fullTypeName = typeof(NameAgeComponent).FullName;
//                componentBlueprint.members = new [] {
//                    new SerializableMember("xxx", "publicFieldValue"),
//                    new SerializableMember("publicProperty", "publicPropertyValue")
//                };
//                componentBlueprint.CreateComponent(_entity);
//            });
//        };

//        context["EntityIndex"] = () => {

//            it["no entity with key"] = () => printErrorMessage(() => {
//                createPrimaryIndex().GetEntity("unknownKey");
//            });

//            it["multiple entities for primary key"] = () => printErrorMessage(() => {
//                createPrimaryIndex();
//                var nameAge = createNameAge();
//                _context.CreateEntity().AddComponent(CID.ComponentA, nameAge);
//                _context.CreateEntity().AddComponent(CID.ComponentA, nameAge);
//            });
//        };
//    }
//}