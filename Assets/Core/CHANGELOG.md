# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.2.0] - 2020-08-30 (Property Binding System : DataBind)
### Added
* Add property and properties bind system on data bind.
    * base value type: boolean, int, string, float.
    * ui value type: [Text].text, [Image].image, [RawImage].texture, [Graphic].color.

### Change
* BaseFrame.Editor.asmdef contains only editor platforms.

## [1.1.1] - 2020-08-25
### Changed
* Simplified translation system structure.

### Fixed
* Find [TargetContext] component in inactive parents at [Bindable] class.
* Fix [LocalizingComponentForText] class name.

## [1.1.0] - 2020-08-22 (Component Binding System : DataBind)
### Added
* [GameObjectArrayBind] class replaces the removed [ArrayBind] class.
* Process of convert to component array value from bound game object array.
* Use value name as Key when use empty [ResolverAttribute] constructor.
* Add public access modifier at field targets that using [ResolverAttribute].
* Add ui Component data bind system.
* Change bind type from 'UIBehaviour' to 'Component'.

### Changed
* Rename some classes for exact purpose.
* Find [Context] component in only parents game object.
* Bindable target type has Changed from [Component] to [Object].
* Change find a way Context in 'Bindable' class.
* Change the processing structure of the ResolveUIAttribute.
* Rename path from UIDataBind to DataBind.

### Fixed
* Pass GetComponent process if element type is [GameObject] type in game object array type.
* Find [Bindable] component in inactive children at [Context] class.

### Removed
* Remove [(Text, Image, RawImage)ArrayBind] class.

## [1.0.2] - 2020-08-08
* All base frame sources load from github.
* change namespace from 'GlobalText' to 'Localization'.
* separate tsv source with 'TableData' and 'Localization'.

## [1.0.1] - 2020-07-01
* Change directories to all scripts by namespace.
* Add ViewLayoutLoader, ViewLayout, AreaBase, ElementBase class.
* Change navigation structure => Scene > View(Page/Popup) > ViewLayout > Area > Element.
* Scene => 씬.
* View => 씬에서 유저에게 보여지는 개별 페이지 혹은 팝업.
* ViewLayoutLoader => 페이지에서 규칙적으로 호출되는 ViewLayout을 호출하기 위한 컴포넌트.
* ViewLayout => 페이지에서 용도에 따라 다르게 보여지는 큰 구성단위.
* Area => 하나의 데이터 모델을 가지고 보여지는 작은 구성단위.
* Element => View에서 중복된 구조의 정보를 보여주기 위한 최소 구성단위.

## [1.0.0] - Pre Versioning
* Popup 공통 애니메이션 추가.
* 전체 사이즈 변경 팝업 애니메이션.
* 세로 사이즈 변경 팝업 애니메이션.
* 모든 자동 실행 애니메이션을 AnimationBase 상속으로 변경.
* PopupAnimationBase에 종료 UnityEvent 추가.
* Popup생성 MenuItem추가.
* AnimationPosition 클래스 추가. (위치값 변경 애니메이션)
* 유니티 5.6.4f1버전 상승. ( 기존 5.5.2f1 )
* 모든 스크립트에 NameSpace추가. ( KKSFramework, KKSFramework.UI, KKSFramework.Animation, KKSFramework.TableData, KKSFramework.Event )
* 모든 스크립트에 Region 추가. ( Property & Value, UnityMethod, Method, EventMethod )
* Material Offset 클래스 삭제. ( 3D게임에서, 특수한 상황에만 사용함... )
* 공통 사용 Font추가 ( 나눔고딕체, 다음체, 대한체 )
* 다른 게임과 InstanceID가 같은 Scene 삭제.
* 모든 Base 클래스와 Base클래스를 상속한 클래스에 Header, Space Attribute추가.
* 공통 사용 Font추가 ( 청소년체 )
* Editor_AddLocalDataKeyPopup, Editor_RegistXMLFile 클래스를 에디터가 Refresh가 되어도 지속 가능하도록 설계.
* 모든 Editor클래스에 KKSFramework.Editor 네임스페이스 추가.
* Interface 클래스 작성.
* ButtonColorChange 클래스 작성. ( 버튼 하위 오브젝트 색상 일괄 변경 )
* UIButtonBase 클래스가 Unity UI의 Button을 상속한 ButtonColorChange클래스를 상속하도록 변경.
* Editor_UIButtonBase 클래스 작성. ( UIButtonBase클래스의 커스텀 에디터 )
* ToggleColorChange 클래스 작성. ( 토글 하위 오브젝트 색상 일괄 변경 )
* UIToggleBase 클래스 작성 ( 기본 토글에 onClick이벤트 추가 )
* Editor_UIToggleBase 클래스 작성. ( UIToggleBase클래스의 커스텀 에디터 )
* ImageColorFollow와 ImageColorChange의 참조할 UI 영역을 UnityEngine.UI.Image에서 UnityEngine.UI.Graphic으로 변경.
* 모든 UnityEvent사용 시, 조건문에 있는 PersistCount체크 해제.
* ColorChangeChildrenGraphics 클래스 작성. ( 하위의 모든 Graphic의 색상값(Color, ColorOnly, AlphaOnly)을 변경 할 수 있음 )
* Popup에 사용될 알파값 변경 팝업 애니메이션 추가.
* PopupAnimationBase 클래스에 팝업 닫기 실행과 함께 종료 이벤트를 추가할 수 있는 함수 추가.
* PopupAnimationBase 클래스에 uevent_end_once ( 1회성 종료 이벤트 변수 ) 추가.
* UITextBase클래스 작성. ( Text 컴포넌트를 자주 사용하는 옵션으로 초기화 )
* UIButton, UIToggle의 하위 그래픽 색상 변경 옵션의 열거형 변수 Status_Color_Option에 None을 추가해 하위 그래픽들이 영향을 받지 않도록 함.
* UIButton, UIToggle에 Button, Toggle값을 복사하여 적용할 수 있는 ReplaceComponent 함수 생성.
* UIButton, UIToggle의 네임스페이스를 Event에서 UI로 변경.
* UIButtonBase와 UIToggleBase에 이벤트 관리 인터페이스인 IFrameworksButtonBase와 IFrameworksToggleBase 참조.
* UIButtonBase클래스에서 OnClick함수 2번 호출되는 문제 수정.
* UIButtonBase와 UIToggleBase에 클릭 사운드 추가.
* UIButtonBase와 UIToggleBase를 상속한 자식 클래스에서 선언된 변수가 인스펙터 창에 보이게 설정.
* Editor_RegistAudioClip 클래스 작성. ( Audio Clip을 BGM, SFX별로 저장, 로드 )
* SoundBase 클래스 작성. ( Monobehaviour에서 사운드 실행 ).
* Manager_Input과 InputKeyBoard, Key클래스 ( 커스텀 키보드 입력 클래스 ) 작성.
* 모든 Editor 클래스의 메뉴 경로 상수화.
* Editor_PopupCreate 클래스 작성. ( 기본 팝업, 애니메이션 팝업(+2) 생성 )
* Editor_PopupCreate 클래스에 알파값 변경 애니메이션 팝업 생성 추가.
* 모든 Editor 클래스는 KKSFramework.Editor의 네임스페이스를 선언.
* Editor_ChangeBaseUIToFrameworksUI 클래스 작성. ( Unity Button을 KKSFramework.UI.UIButton으로 Unity Toggle을 KKSFramework.UI.UIToggle로 교체함 ).
* PopupAnimationBase에 1회성 팝업 이벤트를 추가하는 함수 추가.
* LocalData 클래스와 포함된 모든 static 함수들을 싱글톤 패턴을 가진 일반 클래스, 함수로 변경.
* LocalData 클래스에서 데이터를 분류하여 세이브, 로드 할 수 있는 식별자 변수 추가.
* Prefab 클래스 작성. ( 프리팹 오브젝트 생성 관리 클래스, 중복 불가. )
* Prefab 클래스에 자동 파괴 시간 인자값을 가진 함수 추가.
* RectTransformAnchor 클래스 작성. ( RectTransform의 간격을 맞춰주는 클래스. )
* RectTransformAnchor 클래스 삭제. ( Horizontal, Vertical Layout으로 대체할 수 있음. )
* UITextBase 클래스에 Outline, Shadow를 추가하는 함수 추가.
* UIImageBase 클래스 작성. ( 이미지 대칭, Outline, Shadow 효과 관리 추가. )
* UITextBase 클래스에 Outline, Shadow 효과 관리 기능 추가.
* JoystickControl 클래스 작성. ( 조이스틱 이동 클래스 )
* JoystickControl 예시 프리팹 작성 및 Prefab 클래스 포함.
* InputKeyboard 예시 프리팹 작성 및 Prefab 클래스 포함.
* Editor_ChangeBaseUIToFrameworksUI 클래스에 Unity Text에서 KKSFramework.UI.UITextBase로 변경해주는 함수 추가.
* Editor_AddPrefabComponent 클래스 작성. ( Transform에 우클릭 메뉴에서 Prefab 클래스를 생성해주는 클래스 )
* Editor_UITextBase 클래스 작성. ( UITextBase클래스의 커스텀 에디터 )
* Editor_UITextBase 클래스를 통해 UITextBase에서 Outline, Shadow컴포넌트를 바로 추가 할 수 있는 버튼 생성.
* Editor_UITextBase 클래스의 Add - 버튼 처리를 다수의 오브젝트에서 가능하게 변경.
* Editor_UIImageBase 클래스 작성. ( UIImageBase클래스의 커스텀 에디터 )
* Editor_UITextBase 클래스에서 Outline, Shadow의 이펙트를 추가할 수 있게 변경.
* Editor_UICreate 클래스 작성. ( UIImage, UIText, UIButton, UIToggle의 오브젝트를 생성해주는 클래스 )
* Editor_UICreate 클래스에서 오브젝트 생성 경로가 현재 선택한 오브젝트가 있을 경우, 선택 오브젝트 하위로 생성되게 변경.
* Editor_UICreate 클래스에서 UI 오브젝트 생성 후 자동으로 생성된 오브젝트를 선택 하도록 변경.
* UITextBase와 UIImageBase클래스가 프리팹에서 생성되었을 경우 Effect 컴포넌트가 숨겨지지 않는 문제 수정.
* BGMSoundPlayer 클래스와 SFXSoundPlayer 클래스에서 각각 StatusBGMKey과 StatusSFXKey로 사운드를 실행 할 수 있도록 변경.
* Editor_RegistAudioClip 클래스에서 데이터 저장시 StatusBGMKey과 StatusSFXKey 이름의 Enum파일을 작성하도록 변경.
* Editor_UICreate 클래스에서 텍스트 컴포넌트 생성시 디폴트 폰트(Arial)로 생성되도록 변경.
* Editor_TextHotKey클래스 삭제.
* AnimationTrembled 클래스 작성. ( 타겟 Transform이 떨리는 애니메이션을 실행하는 클래스 )
* AnimationBase클래스를 AnimationBase클래스(애니메이션 기본 클래스)와 VariableAnimationBase클래스(StartValue, EndValue 포함)로 분할.
* VariableAnimationBase클래스는 AnimationBase클래스를 상속하도록 함.
* AnimationBase의 StartValue, EndValue가 필요없는 AnimationFlicker, AnimationTrembled, AnimationPosition 클래스의 상속 클래스는 AnimationBase로, 필요한 애니메이션 클래스의 상속 클래스는 VariableAnimationBase클래스로 변경.
* LocalData클래스와 포함된 함수를 Static에서 Singtone패턴으로 구현한 일반 클래스, 함수로 변경. 
* 콜라보레이션으로 버전 컨트롤 할 수 있도록 변경.
* GPGSManager 클래스 작성. ( 구글 플레이 게임 센터 플러그인 기능 관리 클래스 )(주석처리)
* UnityAdsManager 클래스 작성. ( UnityAds 플러그인 기능 관리 클래스 )(주석처리)
* NGUIButtonExtension 클래스 작성. ( NGUI UIButton 및 PlaySound 클래스를 포함하고, 버튼 클릭 이벤트를 UnityEvent로 추가한 클래스 )
* NGUIToggleExtension 클래스 작성. ( NGUI UIToggle 및 Toggled Objects, Toggled Components 클래스를 포함한 확장 클래스 )
* GetEnumTypeValue 함수 작성. ( 문자열에 해당하는 제네릭 타입의 enum 타입 리턴하는 함수 )
* SingletoneDontDestroyedMonoClass 클래스 작성. ( Monobehaviour를 상속하지만, 타입을 찾을 수 없을 경우 생성해주고 DontDestroy해주는 클래스 )
* NGUICheckDragAxis 클래스와 NGUIDragScrollViewExtension 클래스 작성. ( NGUI ScrollView를 양쪽 축으로 스크롤 할 수 있도록 하는 클래스 )
* ObjectPoolingManager 클래스 작성. ( 오브젝트 풀링 관리 클래스 )
* CachedComponent 클래스 작성. ( 오브젝트 내 컴포넌트 풀링 클래스 )
* Prefab 클래스를 CachedComponent로 관리 할 수 있도록 변경.  
* ObjectPoolingManager 클래스 수정. ( 오브젝트 풀링되도록 )
* PooledObjectBase 클래스 작성. ( 풀링되는 오브젝트 베이스 클래스 )
* ObjectPoolingManager 풀링 타입 ( Status_PoolingObjectType ) 추가.
* ObjectPoolingManager 풀링 자료구조 Queue로 변경.
* TimeCheckManager 클래스 작성. ( 게임 시간 관리 클래스 )
* TimeCheckBase 클래스 작성. ( 유저로 인해 업데이트되는 게임 시간 체크 베이스 클래스 )
* TimeCheckAutoChecked 클래스 작성. ( 자동으로 업데이트되는 게임 시간 체크 클래스 )
* InventoryManager 클래스 작성. ( 게임 아이템 관리 클래스 )
* InventroyInfoBase 클래스 작성. ( 게임 아이템 개체 관리 클래스 )
* Utility에 단계별 FilledAmount 값 리턴 함수 작성. ( ReturnSteppedFilledAmount )
* XML과 같은 방식으로 관리되는 CSV 데이터 테이블 로드 클래스 작성. ( ReadCSVData.cs, Editor_RegistCSVFile, CSVDataEnumKeys )
* XML, CSV데이터를 로드하는 베이스 클래스 작성. ( LoadTableDataBase.cs )
* ObjectPoolingManager 클래스 리팩토링.
* PooledObjectBase 클래스 리팩토링.
* PooledObjectBase 클래스 리팩토링.
* PrefabComponent 클래스 리팩토링.
* Time 네임스페이스의 예하 클래스 리팩토링.
* 기존 Inventory 네임스페이스의 클래스를 GameSystem.Inventory로 이동.
* 인게임 재화를 관리하는 클래스 작성. ( GameResourcesManager.cs ).
* System.CharacterStatus]
* CharacterStatusBase 클래스 작성. ( 캐릭터의 기본 능력치를 관리하는 클래스 )
* CharacterBattleStatus	클래스 작성. ( 캐릭터의 능력치를 기반으로 전투 상호 작용을 하는 클래스 )
* XML, CSV 파일 등록 팝업에 해당 파일을 기본 설정된 프로그램으로 열 수 있는 버튼 추가.
* 팝업과 비슷하게 메인 판넬을 관리하는 클래스 작성. ( PanelManager.cs )
* 메인 판넬을 관리하는 클래스 작성. ( PanelBase.cs )
* 메인 판넬 하위의 서브 판넬을 관리하는 클래스 작성. ( UIPanelBase.cs )
* NGUISpriteExtension, NGUIUnity2DSpriteExtension의 콜라이더 크기 조정 함수 완벽하게 작동하도록 수정. ( ResizeCollider )
* NGUIButtonExtention 클래스의 하위 위젯 컴포넌트들의 색상을 교체하는 기능을 하는 함수 작성. ( SetChildrenColor )
* 앱 기본 설정 관리 클래스. ( ApplicationManager.cs )
* 보존이 필요한 게임 데이터 클래스 작성. ( GameDataManager.cs )
* 자원 변화 주기 이벤트 추가.
* 자원 데이터를 보존 게임 데이터 클래스에 저장 되도록 수정.
* 인벤토리 데이터를 보존 게임 데이터 클래스에 저장 되도록 수정.
* 캐릭터 사용 스킬 테이블 로드 클래스 작성. ( CSVLoadSkillDataTable.cs )
* 퀘스트 데이터 테이블 로드 클래스 작성. ( CSVLoadQuestDataTable.cs )
* GameDataManager: 퀘스트 관리 부분 추가 및 데이터 입출력 구조 변경.
* 글로벌 텍스트 데이터 테이블 로드 클래스 작성. ( CSVLoadGlobalTextDataTable.cs )
* 캐릭터 유한 상태 기계(FSM) 클래스 작성 캐릭터 1차 베이스. ( CharacterFSMStatusBase.cs )
* 캐릭터 능력치 기반 클래스 작성 캐릭터 2차 베이스. ( CharacterStatusBase.cs )
* 캐릭터 능력치 기반, 상호 작용 클래스 작성 캐릭터 3차. ( CharacterBattleStatus.cs )
* 캐릭터 공격, 방어에 대한 상호 작용 구현.
* 캐릭터 스킬 사용, 스킬 적용 부분 구현.
* 스킬: 능력치 증가/감소 버프 스킬 적용 부분 구현.
* 캐릭터 유한 상태(FS) 유형 세부 분리.
* 캐릭터 방어막 능력치 구현.
* 스킬: 코스트 사용 구현.
* 스킬: 방어막 구현.
* 스킬: 스킬 쿨타임 구현.
* 스킬: 링크 스킬(즉발, 장착, 장착 해제) 구현.
* 캐릭터 장비 아이템 클래스 작성. ( CharacterEquipmentStatus.cs )
* 퀘스트 UI 표시 관리 템플릿 판넬 및 UI 항목 판넬 클래스 작성. ( TemplateUIQuestPanel.cs, TemplateQuestPanel.cs )
* 퀘스트 데이터 관리 클래스 작성. ( QuestManager.cs )
* 글로벌 텍스트 관리 클래스 작성. ( LocalizationTextManager.cs )
* 글로벌 텍스트 컴포넌트 클래스 작성. (GlobalText.cs)
* [UIPopupBase, PopupManager, PanelManager]와 관련된 클래스 => [PageView.NavigationManager, ViewBase, PopupBase, PageBase]로 변경.
* PrefabComponent / PooledObjectBase 클래스의 딜레이 오프 함수 삭제. 
* 오브젝트를 관리할 ObjectPoolingManager 클래스 내 전용 딕셔너리 변수 추가 및 함수 변화. 
* 매니저 클래스 초기화 클래스 작성. ( ManagerInstaller.cs ) 
* 매니저 클래스 구조 변경에 따른 Base 클래스 작성. ( ManagerBase.cs, ComponentBase.cs )
* ManagerClass 규약 : 데이터의 가공에 대한 관리 클래스, HelperClass가 상속 받아 사용할 변수, 함수들은 protected로, 이외는 private로 정의. ManagerBase.InitManager함수로 들어온 ComponentBase를 ComponentClass 형변환.
* HelperClass 규약 : 데이터 접근에 대한 관리 클래스, 기능 접근이 필요한 ManagerClass의 함수를 internal new로 재 정의.
* ComponentClass 규약 : ManagerClass에서 필요한 컴포넌트를 정의.
* 매니저 클래스 구조 변경 [HelperClass : ManagerClass<HelperClass> (ComponentClass => ComponentBase as ComponentClass) : ManagerBase<T> : SingletonNonMonoClass (ComponentBase)]
* 매니저 클래스 구조 변경 완료. 
* 매니저 클래스 구조 protected로, 헬퍼 클래스 구조 internal new로 재정의.
* 게임 로컬 데이터 관리 구조 변경 로컬 데이터 로드 인터페이스 구현.
* 게임 데이터 Json파일로 저장되도록 변경.
* NGUI 삭제 및 관련 클래스 모두 삭제 모든 UI는 UGUI로 구현.
* 사운드 구동 방식 SoundPlayHelperClass로 통합.
* 모든 UI클래스 리팩터링. 
* 페이지/팝업 호출 구조 변경.
* 페이지/팝업 호출 구조 변경 (Async, Unitask 추가).
* await OnPush -> 뷰가 화면에 보이기전 사전 처리.
* OnShowed -> 뷰가 화면에 보이고 나서 처리.
* await OnPop -> 뷰가 팝되기 전 사전 처리.
* await OnForeground -> 페이지가 전환되어 나타나기전 사전 처리.
* await OnBackground -> 페이지가 전환되어 사라지기전 사전 처리.
* OnHid -> 뷰가 화면에 사라지고 나서 처리.
* 페이지(A) 출현await A Push -> A Show -> A OnShowed
* 페이지(A) > 페이지(B) 전환 await A BackGround -> await A Hide -> A OnHid -> await B Push -> await B Show -> B OnShowed
* 페이지(B) > 페이지(A) 복귀 await B Pop -> await B Hide -> A Foreground -> A Show -> A OnShowed
* 모든 Helper 클래스 삭제.
* 플랫폼 서비스 클래스 추가(PlatformService).
* UniRx.Async 플러그인 추가.
* Zenject 플러그인 추가.
* 모든 클래스 코드 클린업.
* 기본 씬 추가.
* 페이지, 팝업 Show, Hide 이펙트 추가.
* 페이지, 팝업 호출 구조 수정.