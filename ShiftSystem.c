int currentPlayer = 1;
int player1Score = 0;
int player2Score = 0;

void switchTurn() {
    currentPlayer = (currentPlayer == 1) ? 2 : 1;
}

void addScore(int points) {
    if (currentPlayer == 1) {
        player1Score += points;
    } else {
        player2Score += points;
    }
}
