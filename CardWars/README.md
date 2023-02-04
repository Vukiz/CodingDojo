# CodingDojo
https://github.com/gigasquid/wonderland-clojure-katas/tree/master/card-game-war - idea
## First Kata
This kata is a version of the classic card game [War](http://en.wikipedia.org/wiki/War_%28card_game%29).

The rules of this card game are quite simple.

- There are two players.
- The cards are all dealt equally to each player.
- Each round, player 1 lays a card down face up at the same time that
  player 2 lays a card down face up.  Whoever has the highest value
  card, wins round and takes both cards.
- The winning cards are added to the bottom of the winners deck.
- Aces are high.
- If both cards are of equal value - three cards are dealt from each hand face down and then 1 more face up to war again. the winner takes all the cards. If this ties repeat the process again.
- The player that runs out of cards loses.
- Cards are added to the back in the following order: cards are added in turns, starting from first loser card then first winner card and so on.
Example: {A, 10} VS {2, 3} => {10, 2, A} VS {3}

### Instructions

- Clone or fork this repo
- In this kata, you will be prompted to fill in your own tests.
- Make the tests pass!
