#include <iostream>
#include <string>
using namespace std;

class PlayerProfile {
public:
    string name;
    int coins;

    PlayerProfile(string playerName) {
        name = playerName;
        coins = 1000;
    }

    void earnCoins(int amount) {
        coins += amount;
    }

    void spendCoins(int amount) {
        if (coins >= amount) {
            coins -= amount;
        }
    }
};
