import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TarefasComponent } from './tarefas.component';
import { OAuthService } from '../services/oauth.service';
import { TarefasService } from '../services/tarefas.service';
import { ToastrService } from 'ngx-toastr';
import { ChangeDetectorRef } from '@angular/core';
import { Tarefa } from '../models/tarefa.model';
import { of, throwError } from 'rxjs';

describe("TarefasComponent", () => {
    let component: TarefasComponent;
    let fixture: ComponentFixture<TarefasComponent>;

    let oAuthServiceMock = {
        getAcessToken: jest.fn().mockReturnValue(of("token123"))
    }

    let tarefasServiceMock = {
        listAll: jest.fn(),
        getById: jest.fn().mockImplementation((id) => {
            return of(new Tarefa("Titulo1", "Descricao1", "Baixa"));
        }),
        create: jest.fn(),
        update: jest.fn(),
        delete: jest.fn()
    }

    let toastrMock = {
        success: jest.fn(),
        error: jest.fn()
    }

    let changeDetectionMock = {
        markForCheck: jest.fn()
    }

    beforeEach(async () => { 
        jest.clearAllMocks();

        await TestBed.configureTestingModule({
            declarations: [TarefasComponent],
            providers: [
                { provide: OAuthService, useValue: oAuthServiceMock},
                { provide: TarefasService, useValue: tarefasServiceMock},
                { provide: ToastrService, useValue: toastrMock},
                { provide: ChangeDetectorRef, useValue: changeDetectionMock},
            ]
        }).compileComponents()
        
        fixture = TestBed.createComponent(TarefasComponent)
        component = fixture.componentInstance
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
                jest.spyOn(Storage.prototype, 'setItem');

                component.ngOnInit();

                expect(localStorage.setItem).toHaveBeenCalledWith('token', 'token123')
            })

            it ("should call listAll", () => {
                const listAllSpy = jest.spyOn(component, "listAll")

                component.ngOnInit();

                expect(listAllSpy).toHaveBeenCalled()
            })
        })

        describe("service returns error", () => {
            it ("should call handleError", () => {
                const handleErrorSpy = jest.spyOn(component, "handleError")
                oAuthServiceMock.getAcessToken.mockReturnValue(
                    throwError(() => new Error("error"))
                )

                component.ngOnInit();

                expect(handleErrorSpy).toHaveBeenCalled()
            })
        })    
    })
})