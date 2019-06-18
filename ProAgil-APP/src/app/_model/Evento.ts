import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
import { Palestrante } from './Palestrante';

export interface Evento {
    eventoId: number;
    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number;
    imagemURL: string;
    imagemURL2: string;
    lote: string;
    telefone: string;
    email: string;
    lotes: Lote[];
    redesSociais: RedeSocial[];
    palestranteEventos: Palestrante[];
}
