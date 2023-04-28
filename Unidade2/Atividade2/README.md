[CG_Biblioteca]:            ../../CG_Biblioteca/              "CG_Biblioteca"  
[CG_Biblioteca_Matematica]: ../../CG_Biblioteca/Matematica.cs "CG_Biblioteca_Matematica"  

# Unidade 2: OpenGL (OpenTK) - atividade  

Lembretes:

- cada questão deste trabalho deve ser separada em novas pastas e projetos executados separadamente. Obrigatoriamente devem usar como base o projeto de exemplo **[CG_N2](./CG_N2/)**. Lembrem que este projeto também usa o projeto [CG_Biblioteca] disponível na pasta raiz do GitHub da equipe.
- usem as sub-pasta da Unidade2 do GitHub da sua equipe para desenvolver/entregar o seu código. No caso:
  - [atividade 1](#1-explorar-o-uso-da-primitiva-gráfica-ponto-no-sru) na pasta [CG_N2_1](CG_N2_1)  
  - [atividade 2](#2-primitivas-geométricas) na pasta [CG_N2_2](CG_N2_2)  
  - [atividade 3](#3-sr-palito-dando-seus-primeiros-passos) na pasta [CG_N2_3](CG_N2_3)  
  - [atividade 4](#4-spline) na pasta [CG_N2_4](CG_N2_4)  
  - [atividade 5](#5-bbox-dos-círculos) na pasta [CG_N2_5](CG_N2_5)  

## 1. Explorar o uso da primitiva gráfica ponto no SRU  

Implemente uma aplicação para desenhar um círculo no centro do Sistema de Referência do Universo (SRU), com raio de valor 0.5. Utilize 72 pontos (com um tamanho do ponto de 5) simetricamente distribuídos sobre o perímetro do círculo, de forma que o resultado final seja o mais parecido com o código: [CG_N2_1_win10-x64.zip](./CG_N2_2/CG_N2_1_win10-x64.zip "CG_N2_1_win10-x64.zip"). Neste caso crie uma nova classe com o nome ```Circulo``` em ```Circulo.cs```, e usem como base as classes: [Ponto.cs](./CG_N2/Ponto.cs), [SegReta.cs](./CG_N2/SegReta.cs), [Retangulo.cs](./CG_N2/Retangulo.cs) e [Poligono.cs](./CG_N2/Poligono.cs).  

Observações:  

- desenhe somente os eixos positivos x e y, cada um com comprimento igual a 0.5;  
- experimente mudar a cor de fundo da tela e a cor de desenho dos pontos para ficarem igual a figura a cima;  
- utilize as funções sin(ang) e cos(ang) para calcular os pontos do círculo da Classe [CG_Biblioteca_Matematica] fornecida;  
- não é permitido usar o comando circle do OpenGL e nem outra implementação que não usem as funções da classe [CG_Biblioteca_Matematica].  

## 2. Primitivas Geométricas  

Nesta aplicação a ideia é explorar a utilização das “primitivas geométricas” de forma que o resultado final seja o mais parecido com o código: [CG_N2_2_win10-x64.zip](./CG_N2_2/CG_N2_2_win10-x64.zip "CG_N2_2_win10-x64.zip"). Aqui se pode usar a classe [Retangulo.cs](./CG_N2/Retangulo.cs).  

No caso a interação deve ser:  

- para alternar entre as “primitivas geométricas” usem a tecla de “barra de espaço”;  
- as “primitivas geométricas” que devem ser utilizadas são: Points, Lines, LineLoop, LineStrip, Triangles, TriangleStrip e TriangleFan.  

## 3. Sr. Palito, dando seus primeiros passos  

Agora, crie uma nova aplicação com o objetivo de um Segmento de Reta (SR) se transformar no Sr. "Palito". Para isto usem como base as classes [SegReta.cs](./CG_N2/SegReta.cs "SegReta.cs") e ```Circulo.cs``` criada no [Exercício 1](#1-explorar-o-uso-da-primitiva-gráfica-ponto-no-sru "Exercício 1").

Como o Sr. Palito está dando os seus primeiros passos por enquanto ele só consegue:  

- se mover para os lados usando as teclas Q (esquerda) e W (Direita);
- usar as teclas A (diminuir) e S (aumentar) para mudar o seu tamanho (raio);  
- usar as teclas Z (diminuir) e X (aumentar) para girar (ângulo).  

Ao Sr. Palito "nasce" está com os seus "pés" na origem, e sua "cabeça" na posição definida com raio de valor 0.5 e ângulo 45º.  

Ah ... o Sr. Palito não se parece muito com o "desenho" do [segmento de reta](./CG_N2/SegReta.cs) que representa um raio de uma [circunferência](#1-explorar-o-uso-da-primitiva-gráfica-ponto-no-sru) !!  

Se quiser como é o Sr. Palito "engatinhando" nos seus primeiros dias de "vida" olhem o código: [CG_N2_3_win10-x64.zip](./CG_N2_3/CG_N2_3_win10-x64.zip "CG_N2_3_win10-x64.zip").

## 4. Spline  

Já esta aplicação o seu objetivo é poder desenhar uma spline (curva polinomial) que permita alterar a posição (x,y) dos pontos de controle dinamicamente utilizando o teclado.  

No caso a interação deve ser:  

– para mudar entre o ponto de controle selecionado (em cor vermelha) usem a tecla de “barra de espaço”;  
– para mover o ponto selecionado (um dos pontos de controle) usar as teclas C (Cima), B (Baixo), E (Esquerda) e D (Direita);  
– as teclas do sinal de mais (+) e menos (-) podem aumentar e diminui a quantidade de pontos calculados na spline;  
– ao pressionar a tecla R os pontos de controle devem voltar aos valores iniciais;  
– a spline deve ser desenha usando linhas de cor amarela;  
– o poliedro de controle deve ser desenhado usando uma linha de cor ciano.  

**ATENÇÃO**: não é permitido usar o comando spline do OpenGL, sendo só permitido usar UMA das formas de splines “demonstradas em aula”. Ao mover um dos pontos de controle, o poliedro e a spline deve se ajustar aos novos valores deste ponto.  
Veja o exemplo no vídeo a baixo.  

Usem as classes:

- [SegReta.cs](./CG_N2/SegReta.cs "SegReta.cs") para desenhar o poliedro de controle;  
- [Ponto.cs](./CG_N2/Ponto.cs "Ponto$$.cs") para desenhar os pontos de controles; e  
- ```Spline.cs```, crie uma nova classe para representar o objeto gráfico Spline.  

O resultado final deve ser o mais parecido com o código: [CG_N2_4_win10-x64.zip](./CG_N2_4/CG_N2_4_win10-x64.zip "CG_N2_4_win10-x64.zip")  

## 5. BBox dos círculos

E por fim, esta aplicação tem o objetivo de fazer um ```joystick virtual```. Basicamente deve-se desenhar dois círculos (um menor e outro maior) e poder mover o círculo menor, mas sem deixar ele (o centro do círculo menor e o círculo menor) sair dos limites do círculo maior. Para mover o centro do círculo menor usar as teclas C (Cima), B (Baixo), E (Esquerda) e D (Direita).  

Para controlar o movimento do centro do círculo menor deve ser usado (neste ordem):  

– um teste inicial pela BBox interna do círculo maior  
– seguido do cálculo da distância (euclidiana, sem raiz)  

  ![\normalsize&space;distancia=\sqrt{(x_{2}-x_{1})^2+(y_{2}-y_{1})^2}](https://latex.codecogs.com/svg.latex?\normalsize&space;distancia=\sqrt{(x_{2}-x_{1})^2+(y_{2}-y_{1})^2})  

Aqui só use as classes:

- ```Circulo.cs```: para criar os círculos maior e menor (use [Matematica.Dentro](../../CG_Biblioteca/Matematica.cs))  
- ```Ponto.cs``` ([Ponto.cs](./CG_N2/Ponto.cs)): para criar o ponto do centro do círculo menor  
- ```Retangulo.cs``` ([Retangulo.cs](./CG_N2/Retangulo.cs)): para criar a representação visual da BBox interna  

O resultado final deve ser o mais parecido com o código: [CG_N2_5_win10-x64.zip](./CG_N2_5/CG_N2_5_win10-x64.zip "CG_N2_5_win10-x64.zip")  

## Atenção

A avaliação da atividade envolve o desenvolvimento das questões acima apresentadas, mas o mais importante é o integrante da equipe demonstrar conhecimento além do código desenvolvido, também os conceitos apresentados em aula relacionados com a atividade em si.  
Cuide com o prazo de entrega observando o [cronograma](./../../cronograma.md).

## Gabarito

![Gabarito](atividadeGabarito.png "Gabarito")  

----------

## ⏭ [Unidade 3)](../../Unidade3/README.md "Unidade 3")  
