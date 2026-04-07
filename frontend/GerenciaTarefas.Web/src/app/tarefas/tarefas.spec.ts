import { describe, it, expect, beforeEach, vi } from 'vitest';
import { TarefasComponent } from './tarefas.component';
import { OAuthService } from '../services/oauth.service';
import { TarefasService } from '../services/tarefas.service';
import { ToastrService } from 'ngx-toastr';
import { ChangeDetectorRef } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Tarefa } from '../models/tarefa.model';
import { of, throwError } from 'rxjs';

describe("TarefasComponent", () => {
    let component: TarefasComponent;

    let oAuthServiceMock = {
        getAcessToken: vi.fn().mockReturnValue(of("token123"))
    }

    let tarefasServiceMock = {
        listAll: vi.fn().mockReturnValue(of([])),
        getById: vi.fn().mockImplementation((id) => {
            return of(new Tarefa("Titulo1", "Descricao1", "Baixa"));
        }),
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

    
})