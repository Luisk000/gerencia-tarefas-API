import { describe, it, expect, beforeEach, vi } from 'vitest';
import { TarefasComponent } from './tarefas.component';
import { Tarefa } from '../models/tarefa.model';
import { of, throwError } from 'rxjs';

describe("TarefasComponent", () => {
    //#region setup
    let component: TarefasComponent;

    let oAuthServiceMock = {
        getAcessToken: vi.fn().mockReturnValue(of("token123"))
    }

    let tarefasServiceMock = {
        listAll: vi.fn().mockReturnValue(of([])),
        getById: vi.fn(),
        create: vi.fn(),
        update: vi.fn(),
        delete: vi.fn()
    }

    let toastrMock = {
        success: vi.fn(),
        error: vi.fn()
    }

    let changeDetectionMock = {
        markForCheck: vi.fn()
    }

    beforeEach(async () => { 
        vi.clearAllMocks();

        component = new TarefasComponent(
            oAuthServiceMock as any,
            tarefasServiceMock as any,
            toastrMock as any,
            changeDetectionMock as any
        )
    })  
    //#endregion

    describe("ngOnInit", () => {
        beforeEach(() => {           
            component.carregouToken = false           
        })

        it ("should call getAcessToken", () => {
            component.ngOnInit();

            expect(oAuthServiceMock.getAcessToken).toHaveBeenCalled()
        })

        describe("service returns acessToken", () => {
            it ("should set carregouToken to true", () => {
                component.ngOnInit();

                expect(component.carregouToken).toBeTruthy()
            })

            it ("should set 'token' as the acessToken in the localStorage", () => {
                vi.spyOn(Storage.prototype, 'setItem');

                component.ngOnInit();

                expect(localStorage.setItem).toHaveBeenCalledWith('token', 'token123')
            })

            it ("should call listAll", () => {
                const listAllSpy = vi.spyOn(component, "listAll")

                component.ngOnInit();

                expect(listAllSpy).toHaveBeenCalled()
            })
        })

        describe("service returns error", () => {
            it ("should call handleError", () => {
                const handleErrorSpy = vi.spyOn(component, "handleError")
                oAuthServiceMock.getAcessToken.mockReturnValue(
                    throwError(() => new Error("error"))
                )

                component.ngOnInit();

                expect(handleErrorSpy).toHaveBeenCalled()
            })
        })    
    })

    describe("handleError", () => {
        it ("should call handleError with the error", () => {
            const handleErrorSpy = vi.spyOn(component, "handleError")
            const erro = new Error("Erro1")

            component.handleError(erro)

            expect(handleErrorSpy).toHaveBeenCalledWith(erro)
        })      
    })

    describe("listAll", () => {
        var tarefas: Tarefa[];

        beforeEach(() => {
            vi.clearAllMocks();

            tarefas = [
                new Tarefa("tarefa2", "descricao2", "Baixa"),
                new Tarefa("tarefa1", "descricao1", "Alta"),
                new Tarefa("tarefa3", "descricao3", "Alta"),
            ]
            tarefas[0].id = 2;
            tarefas[1].id = 1;
            tarefas[2].id = 3;

            tarefasServiceMock.listAll.mockReturnValue(of([...tarefas]))
        })

        it ("should call listAll on the service", () => {
            component.listAll()

            expect(tarefasServiceMock.listAll).toHaveBeenCalled()
        })

        it ("should sort and set tarefas as the received tarefas", () => {
            var tarefasOrdenadas = [
                new Tarefa("tarefa1", "descricao1", "Alta"),
                new Tarefa("tarefa2", "descricao2", "Baixa"),
                new Tarefa("tarefa3", "descricao3", "Alta"),
            ]
            tarefasOrdenadas[0].id = 1;
            tarefasOrdenadas[1].id = 2;
            tarefasOrdenadas[2].id = 3;

            component.listAll()

            expect(component.tarefas).toEqual(tarefasOrdenadas)
        })  

        it("should mark for check after tarefas are set", () => {
            component.listAll()

            expect(changeDetectionMock.markForCheck).toHaveBeenCalled()
        })

        it("should call handleError when service throws error", () => {
            var handleErrorSpy = vi.spyOn(component, "handleError")
            tarefasServiceMock.listAll.mockReturnValue(
                throwError(() => new Error("Error"))
            )

            component.listAll()

            expect(handleErrorSpy).toHaveBeenCalledWith(expect.any(Error))
        })
    })

    describe("getDadosTarefa", () => {
        var tarefa: Tarefa;

        beforeEach(() => {
            tarefa = new Tarefa("titulo1", "descricao1", "Baixa")
            tarefa.id = 1;

            tarefasServiceMock.getById.mockImplementation((id) => {
                if (id == 1)
                    return of(tarefa);

                return throwError(() => new Error("Error"))
            })
        })

        it ("should set editando and excluindo to false", () => {
            component.getDadosTarefa(tarefa)

            expect(component.editando).toBeFalsy()
            expect(component.excluindo).toBeFalsy()
        })

        it ("should set selectedTarefa as null if tarefa is already selected", () => {
            component.selectedTarefa = tarefa;

            component.getDadosTarefa(tarefa)

            expect(component.selectedTarefa).toBeNull()
        })    
        
        it ("should not call service if tarefa is already selected", () => {
            component.selectedTarefa = tarefa;

            component.getDadosTarefa(tarefa)

            expect(tarefasServiceMock.getById).not.toHaveBeenCalled()
        })
        
        it ("should call getById in service with the correct id", () => {
            component.getDadosTarefa(tarefa)

            expect(tarefasServiceMock.getById).toHaveBeenCalledWith(tarefa.id)
        })

        it ("should set selectedTarefa as tarefa if tarefa is found", () => {
            component.selectedTarefa = null;

            component.getDadosTarefa(tarefa)

            expect(component.selectedTarefa).toEqual(tarefa)
        })

        it ("should change tarefa when another tarefa is selected", () => {
            var tarefa2 = new Tarefa("titulo2", "descricao2", "Alta")
            tarefa2.id = 2;
            component.selectedTarefa = tarefa2;

            component.getDadosTarefa(tarefa)

            expect(component.selectedTarefa).toEqual(tarefa)
        })

        it("should call markForCheck if tarefas is found", () => {
            component.getDadosTarefa(tarefa)

            expect(changeDetectionMock.markForCheck).toHaveBeenCalled();
        })

        it ("should call handleError if tarefa is not found", () => {
            var tarefa2 = new Tarefa("tarefa2", "descricao2", "Baixa");
            tarefa2.id = 2;

            const handleErrorSpy = vi.spyOn(component, "handleError")

            component.getDadosTarefa(tarefa2)

            expect(handleErrorSpy).toHaveBeenCalledWith(expect.any(Error))
        })
    })

    describe("changeEditando", () => {
        beforeEach(() => {
            component.editando = false;
        })

        it ("should set editando to true when receiving false", () => {       
            component.changeEditando()

            expect(component.editando).toBeTruthy()
        })

        it ("should set editando to false when receiving true", () => {
            component.editando = true;
            
            component.changeEditando()

            expect(component.editando).toBeFalsy()
        })

        it ("should set excluindo to false", () => {
            expect(component.excluindo).toBeFalsy()
        })
    })

    describe("changeExcluindo", () => {
        beforeEach(() => {
            component.excluindo = false;
        })

        it ("should set excluindo to true when receiving false", () => {
            component.changeExcluindo()

            expect(component.excluindo).toBeTruthy()
        })

        it ("should set excluindo to false when receiving true", () => {
            component.excluindo = true;

            component.changeExcluindo()

            expect(component.excluindo).toBeFalsy()
        })

        it ("should set editando to false", () => {
            expect(component.editando).toBeFalsy()
        })
    })

    describe("deleteTarefa", () => {
        var tarefa: Tarefa;
        beforeEach(() => {
            tarefa = new Tarefa("titulo1", "descricao1", "Baixa")
            tarefa.id = 1;

            tarefasServiceMock.delete.mockImplementation((id) => {
                if (id === 1)
                    return of(null);

                return throwError(() => new Error("Error"))
            })
        })

        it ("should call delete in service with the correct id", () => {
            component.deleteTarefa(tarefa)

            expect(tarefasServiceMock.delete).toHaveBeenCalledWith(tarefa.id)
        })

        it ("should remove the tarefa from tarefas", () => {
            var tarefa2 = new Tarefa("titulo2", "descricao2", "Alta")
            tarefa2.id = 2;
            component.tarefas.push(tarefa)
            component.tarefas.push(tarefa2)

            component.deleteTarefa(tarefa)

            expect(component.tarefas).not.toContain(tarefa)
            expect(component.tarefas).toContain(tarefa2)
        })

        it("should call toastr with success", () => {
            component.deleteTarefa(tarefa)

            expect(toastrMock.success).toHaveBeenCalled()
        })

        it("should mark for check", () => {
            component.deleteTarefa(tarefa)

            expect(changeDetectionMock.markForCheck).toHaveBeenCalled()
        })

        it("should call handle error when service returns error", () => {
            var handleErrorSpy = vi.spyOn(component, "handleError")
            tarefasServiceMock.delete.mockReturnValue(
                throwError(() => new Error("Error"))
            )

            component.deleteTarefa(tarefa)

            expect(handleErrorSpy).toHaveBeenCalledWith(expect.any(Error))
        })
    })

    describe("confirmarEdicao", () => {
        var updatedTarefa: Tarefa;
        beforeEach(() => {
            component.tarefas = []
            updatedTarefa = new Tarefa("titulo1 updated", "descricao1 updated", "Alta")
            updatedTarefa.id = 1

            tarefasServiceMock.update.mockReturnValue(of(null))
        })

        it ("should set editando to false", () => {
            component.editando = true;
            component.confirmarEdicao(updatedTarefa)

            expect(component.editando).toBeFalsy()
        })

        it ("should set the selected tarefa as this tarefa", () => {
            component.selectedTarefa = new Tarefa("titulo1", "descricao1", "Alta")

            component.confirmarEdicao(updatedTarefa)

            expect(component.selectedTarefa).toEqual(updatedTarefa)
        })

        it ("should call update in the service", () => {
            component.confirmarEdicao(updatedTarefa)

            expect(tarefasServiceMock.update).toHaveBeenCalledWith(updatedTarefa)
        })

        it ("should update tarefa", () => {
            var tarefa = new Tarefa("tarefa1", "descricao1", "Alta")
            tarefa.id = 1
            component.tarefas.push(tarefa)

            component.confirmarEdicao(updatedTarefa)

            expect(component.tarefas[0]).toEqual(updatedTarefa)
        })

        it ("should call toastr with success", () => {
            component.confirmarEdicao(updatedTarefa)

            expect(toastrMock.success).toHaveBeenCalled()
        })

        it ("should mark for check", () => {
            component.confirmarEdicao(updatedTarefa)

            expect(changeDetectionMock.markForCheck).toHaveBeenCalled()
        })

        it ("should call handleError when service returns error", () => {
            const handleErrorSpy = vi.spyOn(component, "handleError")
            tarefasServiceMock.update.mockReturnValue(
                throwError(() => new Error("Error"))
            )

            component.confirmarEdicao(updatedTarefa)

            expect(handleErrorSpy).toHaveBeenCalledWith(expect.any(Error))
        })
    })

    describe("closeTarefas", () => {
        it ("should set selectedTarefa to null", () => {
            component.closeTarefas()

            expect(component.selectedTarefa).toBeNull()
        })

        it ("should set editando and excluindo to false", () => {
            component.closeTarefas()

            expect(component.excluindo).toBeFalsy()
            expect(component.editando).toBeFalsy()
        })
    })
})