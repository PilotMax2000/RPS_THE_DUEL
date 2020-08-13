# RPS_THE_DUEL

<b>"(Rock-Paper_Scissors) The Duel"</b> - is an implementation of a game "Rock-Paper-Scissors", but in turn-based RPG style.
Currently, the game has a hot-seat mode (Player VS Player), and single-mode against AI (very simple, based on random).

<b>The goal</b> of each battle (duel) is quite simple - just kill the opponent and you win.

Current functionality is very basic, but game logic is built in the way, that gives the possibility to easily add, for example,
more states in battle (more complex Element choosing, or Items usage) due to used State pattern.
Or add more complex UI elements interactions due to used Observer pattern.

Also, currently used in-game Stats are not numerous - characters have only HP and Damage stats, 
but due to used implementation, it makes possible to quickly add experience/level up system
(which is partly working, but have no use, currently, in gameplay), bonus stats from weapon/items or adding new stats.

The main logic script of the battle is <b>BattleStatesHandler</b>, it also has the info about main battle states.
Just follow it and all the triggered events and you will see, how everything works.

<b>Prefered screen ratio/resolution</b> => 16:9/FullHD

<b>Used Unity version:</b> 2019.3.10

<i>All used graphics (UI & Skyboxes), VFX, SFX, 3D models and textures were taken from open sources or were bought.
All scripts (excepts those from folder "3d party library" and LazyValue.cs) were created by myself.</i>
