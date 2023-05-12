# Unidade 3: Conceitos Básicos - atividade  

<!--
avisar para postar nas pastas do GitHub
-->

Lembretes:

- todas as questões deste trabalho devem ser desenvolvidas para serem executadas em um **único** projeto.  
- Obrigatoriamente devem usar as classes disponibilizadas na Unidade2 junto com as que foram criadas nesta unidade em [CG_N3_Exemplo](./CG_N3_Exemplo/).  
- usem a pasta [CG_N3](./CG_N3/) do GitHub da sua equipe para criar desenvolver/entregar o seu código.  

## O que deve ser feito

O cenário consiste em implementar um Editor Vetorial 2D para "adicionar" e "manipular" polígonos abertos/fechados e convexos/côncavos utilizando somente o teclado e mouse.  

Conhecimento necessário:  

- estruturas simples dinâmicas para armazenar os dados gráficos;  
- algoritmo de seleção: selecionar polígonos côncavos (não entrelaçados);  
- transformações geométricas 2D: movimentar, girar e redimensionar objetos;  
- conceitos básicos de grafo de cena.  

Descrição: o cenário consiste em implementar um Editor Vetorial 2D para "adicionar" e "manipular" polígonos abertos/fechados e convexos/côncavos utilizando somente o teclado e mouse com a biblioteca OpenGL/OpenTK. A aplicação deve permitir "adicionar" interativamente (“clicando” na tela) polígonos sem limite do número de vértices, e sem limite de polígonos. Além disso, cada polígono pode ter sua própria cor e deve ser possível: apagar/mover vértices e apagar/mover polígonos. Estas funções (apagar/mover) devem se interativas (o usuário deve clicar no polígono ou vértice).  

Observação: as funções do OpenGL/OpenTK "glTranslate", "glRotate" e "glScale" não deve ser utilizada para fazer as Transformações Geométricas, e sim deve ser usado a classe "Transformacao4D" associada ao objeto gráfico. Assim o método de desenho do objeto gráfico utiliza "Transformacao4D" junto com as funções do OpenGL/OpenTK "glPushMatrix", "glMultMatrixd"e "glPopMatrix". A justificativa, é que desta forma isolasse o render do OpenGL/OpenTK facilitando, se for o caso, mudar para outra forma de renderizar (por exemplo, DirectX). Para evitar problemas com diferenças de valores entre coordenadas de tela e do espaço gráfico não altere os valores usados no Ortho do exemplo fornecido. Observem que após mover os objetos gráficos usando a matriz de transformação do referido objeto não é possível simplesmente selecionar o objeto usando os seus vértices originais. Tem de pegar o valor das coordenadas de tela (do pixel), converter para o espaço gráfico (do Ortho, se for o caso) e converter para o espaço do objeto gráfico transformado usando as matrizes de transformações do grafo de cena até chegar no objeto desejado.  

## 1. Especificação

\[Peso 0,5] Especifique as classes, métodos e atributos usando Diagrama de Classes.  

## 2. Estrutura de dados: polígono

\[Peso 0,5] Insira e remova polígonos da cena gráfica.  

## 3. Estrutura de dados: vértices

\[Peso 1,0] Ao clicar na tela utilizando o mouse selecione o vértice mais próximo do polígono selecionado para pode mover ou remover o respectivo vértice.  

Atenção: no caso do mover o vértice o valores da coordenada é alterada e não os valores da matriz de transformação.  

## 4. Visualização: rastro

\[Peso 0,5] Exiba o “rasto” ao desenhar os segmentos do polígono.  

## 5. Interação: desenho

\[Peso 1,0] Utilize o mouse para clicar na tela e desenhar polígonos (aberto ou fechado).  

## 6. Interação: cores

\[Peso 0,5] Utilize o teclado (teclas R=vermelho,G=verde,B=azul) para trocar as cores dos polígonos selecionado.  

## 7. Interação: BBox

\[Peso 1,5] Utilizando o mouse selecione um polígono (convexo ou côncavo) usando detecção hierárquica filtrando primeiro pela BBox e, se for interno a BBox então testar usando Scan Line (exibir a BBox do polígono selecionado usando a cor amarela).  

## 8. Transformações Geométricas: translação

\[Peso 1,0] Utilizando o mouse movimente o polígono selecionado.  

Atenção: usar matriz de transformação e não alterar os valores dos vértices dos polígonos.  

## 9. Transformações Geométricas: escala

\[Peso 1,0] Utilizando o mouse redimensione o polígono selecionado em relação ao centro da sua BBox.  

Atenção: usar matriz de transformação e não alterar os valores dos vértices dos polígonos.  

## 10. Transformações Geométricas: rotação

\[Peso 1,0] Utilizando o mouse gire o polígono selecionado em relação ao centro da sua BBox.  

Atenção: usar matriz de transformação e não alterar os valores dos vértices dos polígonos.  

## 11. Grafo de cena: selecionar

\[Peso 0,5] Permita adicionar polígonos “filhos” num polígono selecionado utilizando a estrutura do grafo de cena.  

Atenção: usar matriz de transformação global para acumular transformações de acordo com o grafo de cena.  

## 12. Grafo de cena: transformação

\[Peso 1,0] Considere a transformação global ao transformar (translação/escala/rotação) um polígono “pai”.  

Atenção: usar matriz de transformação global para acumular transformações de acordo com o grafo de cena.  

## Atenção

A avaliação da atividade envolve o desenvolvimento das questões acima apresentadas, mas o mais importante é o integrante da equipe demonstrar conhecimento além do código desenvolvido, também os conceitos apresentados em aula relacionados com a atividade em si.

## Gabarito

![Gabarito](atividadeGabarito.png "Gabarito")  

----------

## ⏭ [Unidade 4)](../Unidade4/README.md "Unidade 4")  
