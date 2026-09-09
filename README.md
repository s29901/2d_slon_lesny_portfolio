# Museum Adventure (working title)

A short 2D point-and-click adventure about a boy who visits a natural history
museum, finds the skeleton of a forest elephant missing its bones, goes to the
excavation site to dig them up, and comes back to see the exhibit completed.

All artwork — backgrounds, characters, UI and animation — is hand-drawn.

> **Status:** work in progress. Originally a university project, currently being
> reworked into a portfolio piece.

## Tech

| | |
|---|---|
| Engine | Unity 2022.3 LTS |
| Render pipeline | Universal RP (2D Renderer) |
| Language | C# |
| Art | Procreate / Photoshop, frame-by-frame animation |

## Scenes

| Scene | Role |
|---|---|
| `home_page` | Main menu, credits, entry point (holds the persistent `GameManager`) |
| `SampleScene` | The museum — exploration, info panels about the animals |
| `2_scene` | The excavation site — collect six bone fragments |

## Structure

```
Assets/
  Scenes/      three playable scenes
  Scripts/     gameplay, UI, camera, dialogue
  Materials/   sprites and textures
  animation/   animation clips and controllers
  music/       music and sound effects
_source/       raw PSD / video sources, not tracked in git
```

## Running

Open the project in Unity 2022.3 LTS and start from the `home_page` scene —
the persistent game state lives there.

## Credits

Concept, art, animation and code — Alina Shcherbin.
Sound effects and music from open libraries.
