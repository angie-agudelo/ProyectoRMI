/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Client;

import Interface.EmpInterface;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author ANGIE
 */
public class EmpClient {

    public static void main(String[] args) throws IOException {
        int n, m;
        double[][] matriz;
        double[] vectorEmp;
        double[] vectorMes;
        double total;
        String continuar;
        BufferedReader buf = new BufferedReader(new InputStreamReader(System.in));
        do {
            System.out.println("Desea continuar");
            continuar = buf.readLine();
            if (continuar.equals("SI")) {
                try {
                    System.out.println("Por favor ingrese la cantidad de empleados");
                    n = Integer.parseInt(buf.readLine());
                    System.out.println("Por favor ingrese la cantidad de meses");
                    m = Integer.parseInt(buf.readLine());
                    System.out.println("-----MOSTRAR MATRIZ LLENA-----");
                    EmpInterface empInterface = (EmpInterface) Naming.lookup("Emp");
                    matriz = empInterface.llenarMatriz(n, m);
                    for (int j = 0; j < n; j++) {
                        for (int i = 0; i < m; i++) {
                            System.out.print("  " + matriz[i][j]);
                        }
                        System.out.println("\t");
                    }
                    System.out.println("-----TOTAL PAGADO POR CADA EMPLEADO-----");
                    vectorEmp = empInterface.calcularTotalEmpleado(n, m, matriz);
                    for (int j = 0; j < n; j++) {
                        System.out.println("Empleado" + (j + 1) + ": " + vectorEmp[j]);
                    }
                    System.out.println("-----TOTAL PROMEDIO MENSUAL-----");
                    vectorMes = empInterface.calcularPromedioMensual(n, m, matriz);
                    for (int i = 0; i < m; i++) {
                        System.out.println("Mes" + (i + 1) + ": " + vectorMes[i]);
                    }
                    System.out.println("-----TOTAL SALARIOS-----");
                    total = empInterface.calcularTotal(n, m, matriz);
                    System.out.println("El total de los salarios es: " + total);
                } catch (NotBoundException | MalformedURLException | RemoteException ex) {
                    Logger.getLogger(EmpClient.class.getName()).log(Level.SEVERE, null, ex);
                }

            }

        } while (continuar.equals("SI"));
    }

}
