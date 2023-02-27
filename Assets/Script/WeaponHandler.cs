using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    public Weapon CurrentWeapon;
    public Transform RightWeaponPosition;
    public Transform LeftWeaponPosition;
    
    protected bool _tryShoot = false;

    protected Movement _movement;
        // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    protected virtual void  Update()
    {
        HandleInput();
        HandleWeapon();
        
    }

    protected virtual void HandleInput()
    {
    }
    
    protected virtual void  HandleWeapon()
    {
        if (CurrentWeapon == null)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool isFlip = (mousePos.x > transform.position.x) ? false : true;

        Vector2 gunPos =  isFlip ?  LeftWeaponPosition.position : RightWeaponPosition.position;
        Vector2 direction = isFlip ? gunPos - mousePos: mousePos - gunPos;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        CurrentWeapon.transform.position =  gunPos;
        CurrentWeapon.transform.localScale = isFlip ? new Vector3(-1,1,1) : Vector3.one;
        CurrentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        CurrentWeapon.IsFlip = isFlip;

        _movement.FlipAnim = isFlip;
        
        if (_tryShoot)
            CurrentWeapon.Shoot();
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (weapon != null )
            CurrentWeapon = weapon;

    }
}
