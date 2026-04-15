using UnityEngine;

public class PickBoss : MonoBehaviour
{
    public static string pickBoss(int floor) {
        int bossNum = Random.Range(0, 3);

        if (floor == 1) {
            switch (bossNum) {
                case 0:
                    return "Libra";
                case 1:
                    return "Gemini";
                case 2:
                    return "Aquarius";
            }
        }
        else if (floor == 2) {
            switch (bossNum) {
                case 0:
                    return "Pisces";
                case 1:
                    return "Cancer";
                case 2:
                    return "Scorpio";
            }
        }
        else if (floor == 3) {
            switch (bossNum) {
                case 0:
                    return "Taurus";
                case 1:
                    return "Virgo";
                case 2:
                    return "Capricorn";
            }
        }
        else if (floor == 4) {
            switch (bossNum) {
                case 0:
                    return "Sagittarius";
                case 1:
                    return "Leo";
                case 2:
                    return "Aries";
            }
        }
        else if (floor == 5) {
            return "Ophiuchus";
        }
        return "";
    }
}
