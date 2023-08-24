using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDownCoreComponent : CoreComponent
{
    [SerializeField] LayerMask whatIsGround;
    BoxCollider2D _playerContactCollider; //ref player collider to make it trigger when fall down

    public bool _canJumpDown //bool check if it can jump or not by shoot ray to below 4 block  to prevent it detect current ground and make it 4 cuz in stair force entity updown is bug when jumpdown it stuck to ground
    {
        get => Physics2D.Raycast(transform.position + new Vector3(0, -4f, 0), Vector2.down, 10, whatIsGround);
        private set => _canJumpDown = value;
    }

    protected override void Awake()
    {
        base.Awake();

        _playerContactCollider = _core.transform.parent.GetComponent<BoxCollider2D>(); //fetch collider from parent of core 
    }

    public void EnableTriggerCollider() //this call when recieve input S and press spacebar will set player collider trigger true
    {
        _playerContactCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision) //if pass through the platform that jump down will set trigger back to false so it can contact ground collider below
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Stair")) //can jump down pass ground and stair
        {
            _playerContactCollider.isTrigger = false;
        }
    }
}
