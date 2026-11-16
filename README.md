# PatchZone Internal • Unity Mono Ingame Mod & Framework v2.0

Menu interno para R.E.P.O desenvolvido em C# rodando direto na Unity via IMGUI e reflexão dinâmica (.NET Reflection). Não precisa de overlay externo, DirectX hook externo nem executável secundário — roda 100% embutido na pipeline de render da engine.

---

## 🚀 Novidades da v2.0

- **Painel Moderno Integrado**: Interface escura redesenhada (estilo Obsidian & Neon Cyan) direto no `OnGUI`, com abas laterais, toggles modernos, steppers numéricos e suporte a arrastar com o mouse.
- **Visuals (ESP) Totalmente Restaurado**:
  - **Loot & Relíquias**: Caixas delimitadoras com destaque em dourado pra itens valiosos (>$3.000), filtro de preço mínimo, distância e valor em dólar na tela.
  - **Monstros & Inimigos**: ESP com nome da entidade, distância e barra de vida dinâmica com interpolação de cor (verde -> vermelho).
  - **Extração**: Ponto de entrega com status da porta (trancado/aberto), cota da rodada restante e indicador de distância.
- **Blindagem de FPS (Fix do drop de 400 -> 30 FPS)**:
  - Cache estático de assemblies e ponteiros de reflexão (elimina varreduras repetidas de `GetAssemblies` e `GetType`).
  - Pipeline de renderização **Zero-Allocation**: eliminadas as alocações de memória na heap durante o desenho do ESP, evitando que o Garbage Collector trave o jogo.
  - Frustum culling antecipado: descarta objetos atrás da câmera antes de rodar qualquer cálculo ou `GetComponent`.
  - Desacoplamento de rotinas pesadas em tickers de 10 Hz e 0.67 Hz (física e kinematics rodam a 400 FPS sem gargalo).
  - Proteção Singleton no `Loader`: destrói clones se a DLL for injetada mais de uma vez sem fechar o jogo.

---

## 🛠️ Recursos Principais

- **Jogador**: God Mode, estamina infinita, pulo contínuo (Air Jump), velocidade customizável (andar/correr/agachar), super pegada (Super Grab), invisibilidade e iluminação total do mapa (Fullbright).
- **Sessão & Economia**: Trava de dinheiro em $999K, loja 100% gratuita, liberação de itens bloqueados por nível, extração imediata e baterias infinitas para equipamentos.
- **Spawners**: Gerador de orbes de sucata (pequena, média, grande) e equipamentos táticos/armas direto no inventário.
- **Cosméticos**: Desbloqueio completo de todos os chapéus, trajes e skins salvos permanentemente no perfil.

---

## ⌨️ Atalhos

| Tecla | Função |
|---|---|
| `Insert` / `F1` / `Delete` | Abrir / Fechar o menu |
| `Espaço` | Pulo Infinito no ar (quando ativado) |

---

## 🔧 Como Compilar

Você pode compilar direto pelo terminal usando o script `build.ps1` ou chamando o compilador C# (`csc.exe`):

```bash
csc.exe /target:library /out:"CheatMenu.dll" \
  /nostdlib /noconfig \
  /r:"REPO_Data\Managed\mscorlib.dll" \
  /r:"REPO_Data\Managed\netstandard.dll" \
  /r:"REPO_Data\Managed\UnityEngine.dll" \
  /r:"REPO_Data\Managed\UnityEngine.CoreModule.dll" \
  /r:"REPO_Data\Managed\UnityEngine.IMGUIModule.dll" \
  /r:"REPO_Data\Managed\UnityEngine.InputLegacyModule.dll" \
  /r:"REPO_Data\Managed\UnityEngine.PhysicsModule.dll" \
  /r:"REPO_Data\Managed\UnityEngine.TextRenderingModule.dll" \
  menu.cs
```

---

## 👤 Desenvolvedor & Contato

- **Desenvolvedor:** Kayke
- **YouTube:** [@PatchZoneBR](https://www.youtube.com/@PatchZoneBR)
- **Discord:** `kayke_v1`
