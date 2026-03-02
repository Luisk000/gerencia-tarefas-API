export class Tarefa {
    id!: number;
    titulo!: string;
    descricao?: string;
    status?: string;
    prioridade?: string;
    data_criacao?: Date;

    todos_status: string[] = [];
    todas_prioridades: string[] = [];

    constructor(titulo: string, descricao: string, prioridade: string, status?: string){
        this.titulo = titulo;
        this.descricao = descricao;
        this.prioridade = prioridade;
        this.status = status;
    }
}