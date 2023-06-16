# BulletCarnival
FPS Shooter game with a 3rd person camera view for PC

## TODO List:

### Asset Search and Acquisition:
- Search for suitable 3D models for the player character, enemies, and weapons and animations
    - [x] Player 3d + animations: idle, walk, run, shoot*
    - [x] Enemy 3d + animations: idle, walk, melee*
    - [ ] Pistol 3d + animations: shoot, reloading
    - [x] Automatic 3d + animations: shoot, reloading
    - [ ] Sniper rifle + animations: shoot, reloading

- Find appropriate textures, images, and animations for the game elements
    - [x] Buttons for main menu
    - [x] Background image
    - [ ] Inventory
    - [ ] Buttons for in game actions: open/close inventory
    - [x] Reloading of weapon
    - [x] Environment: boxes, walls

### Features
- [x] Player movement (WASD, Shift, Space)
- [x] Mouse aiming
- [x] Create enemies(hp, tracking based on detect zone, attacking: meelee, shoot* based on weapon)
- [x] Shooting mechanic via Raycast
- [x] Deal damage to enemy
- [ ] Inventory system w/ 3 weapons & ammo (pistol, full auto, sniper)
- [x] Main menu UI (Play/Exit) 
- [x] Level UI (Win/Lose) -> Show lose screen when player dies, and win screen on enemies defeated
- [ ] Save system (player progress with win count and lose count) shown after win/lose screen
