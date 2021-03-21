using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START,PLAYER1TRUN,PLAYER2TURN,PLAYER1WON,PLAYER2WON}
public class GameController : MonoBehaviour
{

    public static GameController instance;


    public GameObject playerOne;
    public GameObject playerTwo;

    Vector3 playerOneStartPosition;
    Vector3 playerTwoStartPosition;

    Unit playerOneUnit;
    Unit playerTwoUnit;

    WeaponSystem playerOneWeapon;
    WeaponSystem playerTwoWeapon;

    PlayerMovement playerOneMovement;
    PlayerMovement playerTwoMovement;

    public BattleState state;

    public Text force;
    public Text angle;

    public Text player1HP;
    public Text player2HP;
    public Text gameOver;

    public GameObject P1;
    public GameObject P2;
    public GameObject GameOverScreen;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {

        GameOverScreen.SetActive(false);
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        playerOneUnit = playerOne.GetComponent<Unit>();
        playerTwoUnit = playerTwo.GetComponent<Unit>();

        playerOneWeapon = playerOne.GetComponent<WeaponSystem>();
        playerTwoWeapon = playerTwo.GetComponent<WeaponSystem>();

        playerOneMovement = playerOne.GetComponent<PlayerMovement>();
        playerTwoMovement = playerTwo.GetComponent<PlayerMovement>();
        playerOneStartPosition = playerOne.transform.position;
        playerTwoStartPosition = playerTwo.transform.position;
        player1HP.text = playerOneUnit.currentHP.ToString();
        player2HP.text = playerTwoUnit.currentHP.ToString();
        state = BattleState.PLAYER1TRUN;
        DisableScripts();

    }
    IEnumerator FirePlayerOne()
    {
        playerOneWeapon.Fire();


        playerTwoMovement.enabled = false;
        playerOneMovement.enabled = false;

        yield return new WaitForSeconds(0.1f);





    }
    IEnumerator FirePlayerTwo()
    {
        playerTwoWeapon.Fire();
        playerTwoMovement.enabled = false;
        playerOneMovement.enabled = false;


        yield return new WaitForSeconds(0.1f);


    }

    public void ChangeState()
    {
        player1HP.text = playerOneUnit.currentHP.ToString();
        player2HP.text = playerTwoUnit.currentHP.ToString();
        if (playerTwoUnit.currentHP <= 0)
        {

            state = BattleState.PLAYER1WON;

            DisableScripts();

            PlayerOneWon();
        }
        if (playerOneUnit.currentHP <= 0)
        {

            state = BattleState.PLAYER2WON;

            DisableScripts();

            PlayerTwoWon();

        }
        else

        if (state == BattleState.PLAYER1TRUN)
        {

            state = BattleState.PLAYER2TURN;
            ChangeForce(0);
            ChangeAngle(0);
            DisableScripts();
        }

        else
        if (state == BattleState.PLAYER2TURN)
        {
            state = BattleState.PLAYER1TRUN;
            ChangeForce(0);
            ChangeAngle(0);
            DisableScripts();
        }
    }


    public void FirePlayerOneTurn()
    {
        if (state == BattleState.PLAYER1TRUN)
        {
            StartCoroutine(FirePlayerOne());

        }

    }
    public void FirePlayerTwoTurn()
    {
        if (state == BattleState.PLAYER2TURN)
        {
            StartCoroutine(FirePlayerTwo());

        }
    }
    public void ChangeForce(float newForce)
    {
        if (state == BattleState.PLAYER1TRUN)
        {

            force.text = newForce.ToString();
            playerOneWeapon.ChangeForce(newForce);


        }
        else
        if (state == BattleState.PLAYER2TURN)
        {
            force.text = newForce.ToString();
            playerTwoWeapon.ChangeForce(newForce);



        }
    }
    public void ChangeAngle(float newDegree)
    {
        if (state == BattleState.PLAYER1TRUN)
        {
            angle.text = newDegree.ToString();
            playerOneWeapon.ChangeDegrees(newDegree);

        }
        else
        if (state == BattleState.PLAYER2TURN)
        {
            angle.text = newDegree.ToString();
            playerTwoWeapon.ChangeDegrees(180 - newDegree);


        }
    }

    void PlayerTwoWon()
    {
        GameOverScreen.SetActive(true);
        gameOver.text = "Player Two Won";

    }
    void PlayerOneWon()
    {
        GameOverScreen.SetActive(true);
        gameOver.text = "Player One Won";
    }


    void DisableScripts()
    {
        turn();
        if (state == BattleState.PLAYER1TRUN)
        {
            playerTwoMovement.enabled = false;
            playerOneMovement.enabled = true;

            playerOneWeapon.enabled = true;
            playerTwoWeapon.enabled = false;

        }
        else if (state == BattleState.PLAYER2TURN)
        {
            playerTwoMovement.enabled = true;
            playerOneMovement.enabled = false;

            playerOneWeapon.enabled = false;
            playerTwoWeapon.enabled = true;
        }
    }

    public void restart()
    {
        GameOverScreen.SetActive(false);
        playerOne.transform.position = playerOneStartPosition;
        playerTwo.transform.position = playerTwoStartPosition;

        playerOneUnit.currentHP = playerOneUnit.maxHP;
        playerTwoUnit.currentHP = playerTwoUnit.maxHP;

        playerOneWeapon.ClearTrajectory();
        playerTwoWeapon.ClearTrajectory();

        state = BattleState.PLAYER1TRUN;

    }
    public void exit()
        {
           Application.Quit();
         }


    void turn()
    {
        if (state == BattleState.PLAYER1TRUN)
        {
            P1.SetActive(true);
            P2.SetActive(false);
        }

        if (state == BattleState.PLAYER2TURN)
        {
            P1.SetActive(false);
            P2.SetActive(true);
        }
    }

    


}


    








