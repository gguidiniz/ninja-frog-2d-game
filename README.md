# Ninja Frog's Adventure

## 📝 Descrição do Jogo
**Ninja Frog's Adventure** é um jogo de plataforma 2D vibrante em estilo retrô pixel art. O jogador assume o controle de um sapinho ágil e destemido que deve atravessar três fases repletas de desafios lineares e perigos milimetricamente calculados. O objetivo principal é coletar todas as frutas espalhadas pelos cenários e alcançar o cobiçado troféu de ouro ao final de cada nível para avançar. Com controles responsivos, áudios imersivos de 8-bits e uma interface adaptável, o jogo oferece um ciclo completo de diversão, desafiando o jogador a gerenciar suas vidas até alcançar a glória na tela de vitória final.

## ⚙️ Mecânicas
* **Movimentação 2D Precisa:** Controles horizontais fluidos que permitem desvios rápidos e ajustes de posicionamento no ar.
* **Sistema de Pulo Dinâmico:** Mecânica de pulo realista calibrada com checagem física de solo (*Ground Check*) via detecção de esferas, impedindo pulos infinitos no ar.
* **Coleta e Pontuação Dinâmica:** Itens coletáveis (cerejas) distribuídos pelos mapas que somam pontos à interface em tempo real, acompanhados por partículas visuais e efeitos sonoros retro.
* **Interação Vertical (Escadas):** Elementos de cenário que alteram dinamicamente a física do jogador ao entrar em sua área (gravidade zero) e ativam animações dedicadas de escalada responsivas ao movimento vertical.
* **Obstáculos Mortais (Espinhos):** Armadilhas estáticas e invertidas espalhadas estrategicamente que detectam a colisão com o jogador, ativando uma animação de morte, reduzindo seu contador de vidas e reiniciando a fase.
* **Interface de Usuário Responsiva (HUD):** Painéis modernos construídos com TextMeshPro e Canvas Scaler, garantindo que o contador de frutas e os corações de vida permaneçam perfeitamente alinhados em qualquer resolução (desde Full HD até 2K/4K).
* **Game Loop Integrado:** Transições limpas entre telas de jogo através de índices de construção (*Build Profiles*), contendo uma tela de *Game Over* e uma *Tela Final* de vitória totalmente jogáveis.

## 🎮 Controles
O jogo utiliza o moderno pacote *Input System* da Unity, garantindo compatibilidade e consistência mapeada diretamente no teclado:

* **Mover para Esquerda/Direita:** Teclas `A` / `D` ou `Seta Esquerda` / `Seta Direita`
* **Subir/Descer Escadas:** Teclas `W` / `S` ou `Seta Cima` / `Seta Baixo`
* **Pular:** Tecla `Espaço` (apenas quando estiver tocando firmemente o chão)
* **Reiniciar / Jogar Novamente:** Tecla `Espaço` (quando estiver na tela de *Game Over* ou na *Tela Final* de vitória)

## 🚀 Instruções de Execução

### Executando o Jogo Compilado (Para Jogadores)
1.  Baixe e extraia a pasta compactada do build do jogo (`Jogo_Build_Windows.zip`).
2.  Abra a pasta extraída e certifique-se de que o arquivo executável principal e a pasta de dados que o acompanha (`_Data`) estão no mesmo diretório.
3.  Dê dois cliques no executável do jogo para iniciar a aventura imediatamente no Windows.

### Abrindo o Projeto no Editor (Para Desenvolvedores)
1.  Clone este repositório para o seu ambiente local.
2.  Abra o **Unity Hub** e adicione o projeto localizando a pasta raiz contendo os diretórios `Assets` e `ProjectSettings`.
3.  Certifique-se de possuir os módulos de *Build Support* necessários carregados no seu editor.
4.  Abra a cena inicial localizada em `Assets/Scenes/Fase1.unity` no Project browser.
5.  Clique no botão **Play** no topo do editor para testar, debugar e modificar as fases livremente.

---
Desenvolvido por **Guilherme Diniz**.
