<p align="center">
  <a href="https://oliverrr100.itch.io/tps-lone-hero"><img src="https://github.com/Oliverr100/Oliverr100/raw/main/assets/card-lone-hero.webp" alt="Lone Hero gameplay: a third-person run through a low-poly forest arena." width="100%"></a>
</p>

# Lone Hero

A fast-paced third-person stealth and action-shooter prototype. Stranded in an isolated alien arena and vastly outnumbered, you balance tense line-of-sight stealth with high-stakes projectile combat to secure key items and find the exit. Made in Unity 2022.3 with C#.

**[Play it in your browser on itch.io](https://oliverrr100.itch.io/tps-lone-hero)**

## How to play

- **Move forward and back:** W and S, or the up and down arrows
- **Look and turn:** mouse
- **Jump:** J
- **Shoot:** Space
- **Free the mouse cursor:** Esc
- Collect every item to win. Each enemy takes three hits.

## How it works

- `PlayerBehavior.cs` moves the player with physics, checks for ground with a small overlap sphere before allowing a jump, and fires projectiles.
- `EnemyBehavior.cs` walks a patrol route on a NavMesh. When the player comes within range the enemy gives chase, and it goes back to patrolling once the player gets away.
- `ItemBehavior.cs` and `GameBehavior.cs` track collected items and health, and reveal the win button once every item is found.
- `CameraBehavior.cs` is a mouse-look third-person camera with a clamped vertical angle.

## Built with

Unity 2022.3 LTS (2022.3.62f3), C#, the NavMesh system and TextMesh Pro.

## Open the project

Clone the repo, add the folder in Unity Hub and open it with Unity 2022.3.62f3.

---

More of my games are on my [GitHub profile](https://github.com/Oliverr100) and on [itch.io](https://oliverrr100.itch.io/).
