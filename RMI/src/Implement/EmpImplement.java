/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Implement;

import Interface.EmpInterface;
import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.Random;

/**
 *
 * @author ANGIE
 */
public class EmpImplement extends UnicastRemoteObject implements EmpInterface {

    private int filas;
    private int columnas;

    public int getFilas() {
        return filas;
    }

    public void setFilas(int filas) {
        this.filas = filas;
    }

    public int getColumnas() {
        return columnas;
    }

    public void setColumnas(int columnas) {
        this.columnas = columnas;
    }

    public EmpImplement(int filas, int columnas) throws RemoteException {
        this.filas = filas;
        this.columnas = columnas;
    }

    @Override
    public double[][] llenarMatriz(int n, int m) throws RemoteException {
        double[][] matriz = new double[m][n];
        Random r = new Random();
        for (int j = 0; j < n; j++) {
            for (int i = 0; i < m; i++) {
                matriz[i][j] = r.nextInt(100);
            }
        }
        return matriz;
    }

    @Override
    public double[] calcularTotalEmpleado(int n, int m, double[][] matriz) throws RemoteException {
        double[] vectorEmp = new double[n];
        for (int j = 0; j < n; j++) {
            double acum = 0;
            for (int i = 0; i < m; i++) {
                acum = acum + matriz[i][j];
            }
            vectorEmp[j] = acum;
        }
        return vectorEmp;
    }

    @Override
    public double[] calcularPromedioMensual(int n, int m, double[][] matriz) throws RemoteException {
        double[] vectorMes = new double[m];
        for (int j = 0; j < m; j++) {
            double acum = 0;
            for (int i = 0; i < n; i++) {
                acum = acum + matriz[j][i];
            }
            vectorMes[j] = acum/n;
        }
        return vectorMes;
    }
    @Override
    public double calcularTotal(int n, int m, double[][] matriz) throws RemoteException {
        double acum = 0;
        for (int j = 0; j < n; j++) {
            for (int i = 0; i < m; i++) {
                acum = acum + matriz[i][j];
            }
        }
        return acum;
    }

}
