# froggo-platformer
Projeto para Motores de Jogos 1.

## Como jogar
Utilize as teclas WASD ou setas para mover o personagem horizontalmente. Z ou a tecla de espaço podem ser utilizados para pular. O personagem tem um pulo extra no ar. Se ele coletar um item colecionável, ele reganha o pulo extra caso ele tenha sido usado.

## Instruções de build
Os mapas têm de estar em ordem cronológica nas configurações de build. A plataforma-alvo é Windows e todas as outras configurações foram deixadas em seus valores padrão.

## Bugs conhecidos
- Se o personagem conseguir atingir uma certa velocidade, ele pode atravessar os colisores de terreno e ficar preso dentro de plataformas. Eu não sei porque isso acontece.
- A detecção do chão ainda tem alguns problemas. É possível ganhar um pulo extra correndo na direção de paredes enquanto no ar. Se eu conseguir fazer isso ficar consistente, eu ganho uma mecânica de wall jump de graça.
