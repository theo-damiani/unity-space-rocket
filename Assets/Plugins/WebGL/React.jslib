mergeInto(LibraryManager.library, {
  UnityIsLoaded: function () {
      window.dispatchReactUnityEvent("UnityIsLoaded");
  },
});