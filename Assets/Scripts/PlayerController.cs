using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private enum MovementState {idle, running, jumping, falling, doubleJumping} // Armazena estado de animação.
    private MovementState animState;

    private CharacterInputs characterInputs;
    private InputAction movement;   // Referência para o movimento nos inputs do personagem.
    private Rigidbody2D playerRigidBody; // Referência para o RigidBody2D do jogador.
    private BoxCollider2D boxCol;
    private Animator anim;
    private bool doubleJumping = false; // Usado para checar se deve usar a animação doubleJump.
    private SpriteRenderer sprite;
    private float mov = 0f;

    [SerializeField] private float jumpSpeed = 10f; // Velocidade do pulo.
    [SerializeField] private float runSpeed = 10f;  // Velocidade do movimento.
    [SerializeField] private LayerMask ground; // A camada do chão para verificar grounding.
    [SerializeField] private int extraJumps = 1; // Número de pulos que o jogador tem.
    private int remainingJumps;

    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private AudioSource touchDownSFX;


    private void Awake() 
    {
        characterInputs = new CharacterInputs();
        playerRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
        remainingJumps = extraJumps;
    }


    private void OnEnable() 
    {
        movement = characterInputs.Player.Movement;
        movement.Enable();

        /*
        FAZER: Implementar pulo com botão segurado para pulo mais longo, talvez
        um tap para pulo pequeno (hop) também desse certo.
        */
        characterInputs.Player.Jump.performed += DoJump;
        characterInputs.Player.Jump.Enable();
    }

    /*
    Aplica uma força no personagem toda vez que o botão de pulo for apertado. 
    OBS: AINDA NÃO REAGE SE V0CÊ SEGURA O BOTÃO.
    */
    private void DoJump(InputAction.CallbackContext obj)
    {
        bool groundCheck = isGrounded();

        if (groundCheck || remainingJumps > 0) {
            if (!groundCheck) {doubleJumping = true;}
            jumpSFX.Play();
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpSpeed);
            remainingJumps--;
        }

        // Não estou conseguindo fazer o addForce funcionar:
        //playerRigidBody.AddForce(new Vector2(0, jumpSpeed));
    }

    private void OnDisable() {
        movement.Disable();
        characterInputs.Player.Jump.Disable();
    }

    
    private void FixedUpdate() {
        mov = movement.ReadValue<float>();

        if (isGrounded()){ // isGrounded está sendo checado duas vezes. CORRIGIR.
            remainingJumps = extraJumps;
        }

        playerRigidBody.velocity = new Vector2(mov * runSpeed, playerRigidBody.velocity.y);
        //playerRigidBody.AddForce(new Vector2(movement.ReadValue<float>() * runSpeed, 0));

        UpdateAnimationStates();
    }

    /*
    Muda o estado de animação para refletir como o personagem está no momento.
    */
    private void UpdateAnimationStates(){
        // Deve haver uma forma mais elegante de mudar as animações do que isso:
        // Animações de movimento:
        if (mov > 0){
            animState = MovementState.running;
            sprite.flipX = false;
        } else if (mov < 0) {
            animState = MovementState.running;
            sprite.flipX = true;
        } else {
            animState = MovementState.idle;
        }

        // Pular tem prioridade de animação, portanto será passado aqui embaixo.
        if(playerRigidBody.velocity.y > .1f){
            animState = MovementState.jumping;
        } else if (playerRigidBody.velocity.y < -.1f){
            animState = MovementState.falling;
        }
        if (doubleJumping) {
            animState = MovementState.doubleJumping;
        }

        anim.SetInteger("state", (int)animState);
    }

    // Verifica se o jogador está tocando o chão. Fazer depois: Coyote time.
    private bool isGrounded(){
        return Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    /*
    Reseta os pulos do jogador toda vez que ele coleta um item.
    */
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Collectible")){
            remainingJumps = extraJumps;
        }
    }

    // Toca um sonzinho toda vez que o jogador cai no chão.
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Walkable")){
            touchDownSFX.Play();
        }
    }

    // Para a animação do pulo duplo.
    private void stopDoubleJump(){
        doubleJumping = false;
    }

}
