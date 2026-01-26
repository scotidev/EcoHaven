using UnityEngine;

public class DeathStarScript : MonoBehaviour
{
    int stormtroopers = 150;
    int ewoks = 500;
    int missionsCompleted = 0;
    int totalMissions = 3;
    bool onBattle = true;


    void Start()
    {
        Debug.Log("Você está na Lua de Endor, complete as missões usando os Ewoks para destruir a Estrela da Morte!");

        while (ewoks > stormtroopers && onBattle)
        {
            ewoks -= 50;
            stormtroopers -= 10;
            Debug.Log($"50 Ewoks foram perdidos na batalha. Agora somos " + ewoks + " Ewoks");
            Debug.Log($"10 Stormtroopers foram eliminados. Restam   " + stormtroopers + " Stormtroopers");
        }

        if (missionsCompleted == totalMissions)
        {
            Debug.Log("Missão concluída! A Estrela da Morte foi destruída!");
        }
        else
        {
            Debug.Log("Missão Fracassada! Seu planeta será DESTRUÍDO!!!!");
        }
    }
}
