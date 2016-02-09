﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonContentsDisplay : MonoBehaviour
{
    // prefabs
    public GameObject mineralIcon;
    public GameObject fluidIcon;
    public GameObject plantIcon;
    public GameObject boneIcon;
    public GameObject smoke;

    IList<GameObject> _icons;

    void Start()
    {
        _icons = new List<GameObject>();
    }

    public void AddIconByKeyType(KeyType keyType)
    {
        GameObject prefab = null;

        switch (keyType)
        {
            case KeyType.A:
                prefab = boneIcon;
                break;
            case KeyType.B:
                prefab = plantIcon;
                break;
            case KeyType.C:
                prefab = mineralIcon;
                break;
            case KeyType.D:
                prefab = fluidIcon;
                break;
        }

        if (prefab != null)
        {
            // set prefab position
            prefab.transform.position = new Vector3
            (
                transform.position.x + (_icons.Count - 1),
                transform.position.y,
                transform.position.z + (_icons.Count - 1)
            );

            GameObject newIcon = Instantiate(prefab);
            _icons.Add(newIcon);
        }
    }

    public void ClearIcons
    (
        bool showSmoke = true,
        bool consume = false
    )
    {
        for (var index = _icons.Count - 1; index >= 0; index--)
        {
            var icon = _icons[index];

            // get position of the icon
            var position = icon.transform.position;

            // remove icon from list
            _icons.RemoveAt(index);

            if (showSmoke)
            {
                // add smoke
                smoke.transform.position = new Vector3
                (
                    position.x,
                    position.y,
                    position.z
                );

                Instantiate(smoke);
            }

            if (consume)
            {
                var animator = icon.GetComponent<Animator>();
                animator.Play("element-icon-consumed");
                Destroy(icon, 2);
            }
            else
            {
                Destroy(icon);
            }
        }

        // icons should be all removed now, re-instantiate list
        _icons = new List<GameObject>();
    }
}
