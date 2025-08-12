using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text dialogueText; // Assign in Inspector
    private bool playerNear = false;
    private int dialogueState = 0;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
        }

        // First choice (Goblin or Weapon)
        if (dialogueState == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                dialogueState = 3; // Goblin path
                StartCoroutine(GoblinDialogue());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                dialogueState = 4; // Weapon path
                StartCoroutine(WeaponDialogue());
            }
        }

        // Weapon sub-choice
        if (dialogueState == 5)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(HandleWeaponChoice("Axe"));
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(HandleWeaponChoice("Staff"));
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                StartCoroutine(HandleWeaponChoice("Sword"));
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                StartCoroutine(HandleWeaponChoice("Dagger"));
        }
    }

    void ShowDialogue()
    {
        if (dialogueState == 0)
        {
            dialogueText.text = "Well hello there adventurer!!";
            dialogueState = 1;
        }
        else if (dialogueState == 1)
        {
            dialogueText.text = "What are you here for?\n1. I am trying to get to the goblin's lair.\n2. I am here to buy weapon.";
            dialogueState = 2;
        }
    }

    System.Collections.IEnumerator GoblinDialogue()
    {
        dialogueText.text = "Well that is a very bad idea!";
        yield return new WaitForSeconds(2);
        dialogueText.text = "Adventurer, you shouldn't go there on your own!";
        yield return new WaitForSeconds(2);
        dialogueText.text = "You have to leave.";
        yield return new WaitForSeconds(2);
        dialogueState = 0; // reset so player can talk again
        dialogueText.text = "Press E to talk.";
    }

    System.Collections.IEnumerator WeaponDialogue()
    {
        dialogueText.text = "Oh boy, you are the first customer today!!";
        yield return new WaitForSeconds(2);
        dialogueText.text = "I must offer you a discount!!";
        yield return new WaitForSeconds(2);
        dialogueText.text = "Tell me what catches your eyes in this mess of a table I have!!";
        yield return new WaitForSeconds(2);

        // Show the sub-choice
        dialogueText.text = "Choose your weapon:\n1. Axe - 50% Off\n2. Staff - 50% Off\n3. Sword - 50% Off\n4. Dagger - 50% Off";
        dialogueState = 5;
    }

    System.Collections.IEnumerator HandleWeaponChoice(string weaponName)
    {
        dialogueState = -1; // lock input during choice
        dialogueText.text = $"You chose {weaponName} - 50% Off first buy!";
        yield return new WaitForSeconds(1.5f);
        dialogueText.text = $"{weaponName} Obtained!";
        yield return new WaitForSeconds(1.5f);

        // Reset for next interaction
        dialogueState = 0;
        if (playerNear)
            dialogueText.text = "Press E to talk.";
        else
            dialogueText.text = "";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            dialogueText.text = "Press E to talk.";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            dialogueText.text = "";
            dialogueState = 0; // Reset for next time
        }
    }
}
