﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Grab : MonoBehaviour
{
    public Transform grabedTransform;
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator anim;
    private List<IGrabable> _itemsGrabbed;
    private bool _isFire1Pressed = false;
    private bool _grabbing = false;

    private void Start()
    {
        _itemsGrabbed = new List<IGrabable>();

        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
    }

    void Update()
    {
        // if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        // {
        // if (_grabbing)
        // {
        //     foreach(IGrabable grabable in _itemsGrabbed)
        //     {
        //         Rigidbody rb = grabable.GetTransform().GetComponent<Rigidbody>();
        //         rb.useGravity = true;
        //         rb.isKinematic = false;
        //         rb.transform.SetParent(null);

        //         grabable.IsGrabbed = false;
        //     }

        //     _itemsGrabbed.Clear();
        //     _grabbing = false;
        // }
        // else
        // {
        //     _isFire1Pressed = true;
        //     StartCoroutine(GrabCoroutine());
        // }
        // }

        
    }

    //TODO: Pensar num nome melhor para este método
    public void Apply()
    {
        if (_grabbing)
        {
            foreach (IGrabable grabable in _itemsGrabbed)
            {
                Rigidbody rb = grabable.GetTransform().GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.transform.SetParent(null);

                grabable.IsGrabbed = false;
            }

            _itemsGrabbed.Clear();
            _grabbing = false;
            anim.SetBool("Grabbing", false);
        }
        else
        {
            _isFire1Pressed = true;
            StartCoroutine(GrabCoroutine());
        }
    }

    private IEnumerator GrabCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        _isFire1Pressed = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isFire1Pressed) { return; }

        IGrabable grabable = other.GetComponentInParent<IGrabable>();

        if (grabable != null)
        {
            anim.SetBool("Grabbing", true);
            grabable.GetTransform().SetParent(grabedTransform);
            Rigidbody rb = grabable.GetTransform().GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.transform.localPosition = Vector3.zero;
            rb.transform.localRotation = Quaternion.Euler(Vector3.zero);

            grabable.IsGrabbed = true;
            _grabbing = true;

            _itemsGrabbed.Add(grabable);
            _isFire1Pressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IGrabable grabable = other.GetComponentInParent<IGrabable>();
        if (grabable != null)
        {
            grabable.SetHighlighted(true);
        }
        //else
        //{
        //    Debug.Log("not an IGrabable");
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        IGrabable grabable = other.GetComponentInParent<IGrabable>();
        if (grabable != null)
        {
            grabable.SetHighlighted(false);
        }
    }
}