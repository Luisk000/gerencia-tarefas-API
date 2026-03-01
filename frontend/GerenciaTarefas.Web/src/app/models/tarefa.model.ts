export class Tarefa {
    id!: number;
    titulo!: string;
    descricao?: string;
    status?: string;
    prioridade?: string;
    data_criacao?: Date;

    todos_status: string[] = [];
    todas_prioridades: string[] = [];
}