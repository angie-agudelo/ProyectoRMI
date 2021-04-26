/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Interface;

import java.rmi.Remote;
import java.rmi.RemoteException;

/**
 *
 * @author ANGIE
 */
public interface EmpInterface extends Remote{
    public double[][] llenarMatriz(int n, int m) throws RemoteException;
    public double [] calcularTotalEmpleado(int n, int m, double [][] matriz) throws RemoteException;
    public double[] calcularPromedioMensual(int n, int m, double[][] matriz) throws RemoteException;
    public double calcularTotal(int n, int m, double[][] matriz) throws RemoteException;
}
