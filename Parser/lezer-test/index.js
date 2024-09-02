import { parser } from './parser.js';

// Teste de entrada
const input = "123abc(456)def";
const tree = parser.parse(input);

// Função para imprimir a árvore sintática
function printTree(cursor, indent = 0) {
    console.log(" ".repeat(indent) + cursor.node.type.name);

    if (cursor.firstChild()) {
        do {
            printTree(cursor, indent + 2);
        } while (cursor.nextSibling());
        cursor.parent(); // Volta para o pai depois de visitar os filhos
    }
}

// Criar o cursor a partir da árvore sintática e imprimir a árvore
const cursor = tree.cursor();
printTree(cursor);
