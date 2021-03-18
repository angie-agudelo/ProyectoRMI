/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Server;

import Implement.EmpImplement;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

/**
 *
 * @author ANGIE
 */
public class EmpServer {
    public static void main(String []args) throws RemoteException{
        Registry reg = LocateRegistry.createRegistry(1099);
        EmpImplement empImplement = new EmpImplement(0, 0);
        reg.rebind("Emp", empImplement);
        System.out.println("Servidor iniciado");
    }
    
}
