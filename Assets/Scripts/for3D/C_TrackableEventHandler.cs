/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class C_TrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    public enum arElements { xtrr, vdo }
    public arElements AR_Elmnts;

    public AugmentationsManager augmentationsManager;
    public GameObject AugmentingObj;
    
    public bool firstTime;
    public bool inInterior;
    public bool blueprint;

    public VideoPlayer video;
    public GameObject OnScreenCanvas;
    public GameObject VideoScreenCanvas;
    public GameObject CloseButton;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        inInterior = false;
        blueprint = false;
        firstTime = true;

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        OnScreenCanvas.SetActive(false);
        VideoScreenCanvas.SetActive(false);
        CloseButton.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

            OnTrackingFound();
        }

        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

            switch (AR_Elmnts)
            {
                case arElements.xtrr:
                    augmentationsManager.OffTheReference(AugmentingObj);
                    break;

                case arElements.vdo:
                    OnTrackingLost();
                    PlayFullScreen();
                    break;

                default:
                    OnTrackingLost();
                    break;
            }
        }

        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations

            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        switch (AR_Elmnts)
        {
            case arElements.xtrr:

                if (!inInterior)
                {
                    if (firstTime)
                    {
                        firstTime = false;

                        // Enable rendering:
                        foreach (var component in rendererComponents)
                            component.enabled = true;

                        // Enable colliders:
                        foreach (var component in colliderComponents)
                            component.enabled = true;

                        // Enable canvas':
                        foreach (var component in canvasComponents)
                            component.enabled = true;
                    }
                    else
                    {
                        augmentationsManager.OnTheReference(AugmentingObj);
                    }
                }

                break;

            case arElements.vdo:

                // Enable rendering:
                foreach (var component in rendererComponents)
                    component.enabled = true;

                // Enable colliders:
                foreach (var component in colliderComponents)
                    component.enabled = true;

                // Enable canvas':
                foreach (var component in canvasComponents)
                    component.enabled = true;

                video.renderMode = VideoRenderMode.MaterialOverride;
                VideoScreenCanvas.SetActive(true);
                OnScreenCanvas.SetActive(false);
                CloseButton.SetActive(true);

                break;

            default:
                break;
        }
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        if (AR_Elmnts == arElements.vdo)
        {
            VideoScreenCanvas.SetActive(false);
            OnScreenCanvas.SetActive(true);
        }
    }

    #endregion // PROTECTED_METHODS

    public void PlayFullScreen()
    {
        video.renderMode = VideoRenderMode.CameraNearPlane;
        Debug.Log("PlayingFullScreen");

    }
}
