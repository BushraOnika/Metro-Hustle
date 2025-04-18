﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AOT;
using BovineLabs.Anchor;
using BovineLabs.Anchor.Toolbar;
using JetBrains.Annotations;
using Unity.AppUI.MVVM;
using Unity.AppUI.Navigation;
using Unity.AppUI.UI;
using Unity.Burst;
using Unity.Collections;
using UnityEngine.UIElements;

namespace _src.Scripts.UiTookKit_ECS.Runtime.Classes
{
    [UsedImplicitly]
    public class MyApp : App
    {
        public const string DefaultServiceTabName = "Service";

        private static readonly FunctionPointer<NavigateDelegate> NavigateFunction =
            new(Marshal.GetFunctionPointerForDelegate<NavigateDelegate>(NavigateForwarding));

        private delegate void NavigateDelegate(in FixedString64Bytes screen);

        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "AppUI standard")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "AppUI standard")]
        public static new MyApp current => App.current as MyApp;

        public virtual Panel Panel => (Panel)this.rootVisualElement;

        public virtual string ServiceTabName => DefaultServiceTabName;

        public NavController Controller { get; private set; }

        public NavGraphViewAsset GraphViewAsset { get; set; }

        public VisualElement PopupContainer { get; private set; }

        public VisualElement NotificationContainer { get; private set; }

        public VisualElement TooltipContainer { get; private set; }

        public INavVisualController NavVisualController { get; internal set; }

        public virtual void Initialize()
        {
            Burst.Static.Data = NavigateFunction;

            this.Panel.pickingMode = PickingMode.Ignore;

#if BL_DEBUG || UNITY_EDITOR
            var toolbarView = this.services.GetRequiredService<ToolbarView>();
            this.rootVisualElement.Add(toolbarView);
#endif

            var navigationView = this.services.GetRequiredService<NavigationView>();
            this.rootVisualElement.Add(navigationView);
            this.Controller = navigationView.Controller;

            this.PopupContainer = this.rootVisualElement.Q<VisualElement>("popup-container");
            this.NotificationContainer = this.rootVisualElement.Q<VisualElement>("notification-container");
            this.TooltipContainer = this.rootVisualElement.Q<VisualElement>("tooltip-container");
        }

        /// <summary> A burst compatible way to Navigate to a new screen in the <see cref="GraphViewAsset" />. </summary>
        /// <param name="screen"> The screen to navigate to. </param>
        public static void Navigate(in FixedString64Bytes screen)
        {
            if (Burst.Static.Data.IsCreated)
            {
                Burst.Static.Data.Invoke(screen);
            }
        }

        /// <summary> Navigate to a new screen in the <see cref="GraphViewAsset" />. </summary>
        /// <param name="screen"> The screen to navigate to. </param>
        public void Navigate(string screen)
        {
            this.Controller.Navigate(screen);
        }

        /// <summary> This has been disabled in favour of overriding <see cref="Initialize" />. </summary>
        public sealed override void InitializeComponent()
        {
            base.InitializeComponent();
        }

        [MonoPInvokeCallback(typeof(NavigateDelegate))]
        private static void NavigateForwarding(in FixedString64Bytes screen)
        {
            current.Navigate(screen.ToString());
        }

        private static class Burst
        {
            public static readonly SharedStatic<FunctionPointer<NavigateDelegate>> Static =
                SharedStatic<FunctionPointer<NavigateDelegate>>.GetOrCreate<AnchorApp, FunctionPointer<NavigateDelegate>>();
        }
    }

}