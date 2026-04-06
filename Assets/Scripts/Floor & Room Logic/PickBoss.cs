using UnityEngine;

public class PickBoss : MonoBehaviour
{
    public static string pickBoss(string currentScene) {
        int bossNum = Random.Range(0, 3);

        if (currentScene == "Floor1") {
            switch (bossNum) {
                case 0:
                    return "Libra";
                case 1:
                    return "Gemini";
                case 2:
                    return "Aquarius";
            }
        }
        else if (currentScene == "Floor2") {
            switch (bossNum) {
                case 0:
                    return "Pisces";
                case 1:
                    return "Cancer";
                case 2:
                    return "Scorpio";
            }
        }
        else if (currentScene == "Floor3") {
            switch (bossNum) {
                case 0:
                    return "Taurus";
                case 1:
                    return "Virgo";
                case 2:
                    return "Capricorn";
            }
        }
        else if (currentScene == "Floor4") {
            switch (bossNum) {
                case 0:
                    return "Sagittarius";
                case 1:
                    return "Leo";
                case 2:
                    return "Aries";
            }
        }
        return "";
    }
}
