<?xml version="1.0" encoding="UTF-8"?>

<!--
    Document   : StyleVotaciones.xsl
    Created on : 22 de marzo de 2021, 16:43
    Author     : Leidy Tatiana
    Description:
        Purpose of transformation follows.
-->

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="3.0">
    <xsl:output method="html"/>
    <!-- TODO customize transformation rules 
         syntax recommendation http://www.w3.org/TR/xslt 
    -->
    <xsl:template match="/">
        <html>
            <head>
                <title>StyleVotaciones.xsl</title>
                <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:300,400,500,700" type="text/css"/>
                <link rel="stylesheet" href="https://code.getmdl.io/1.3.0/material.indigo-pink.min.css" />
            </head>
            <body>
                <div class="collapsible">
                    <div>
                        <h1>1 y 2 Información sobre las votaciones de gobernadores </h1>
                    </div>
                    <!--<xsl:variable name="totalAbstencion" select="0"/>-->
                    <xsl:param name="totalAbstencion">0</xsl:param> 
                    <table border="1" class="mdl-data-table mdl-js-data-table">     
                        <thead>
                            <tr>
                                <th>Departamentos</th>         
                                <th>Inscritos</th>
                                <th>Partido 1</th>  
                                <th>Partido 2</th>
                                <th>Partido 3</th>
                                <th>Blanco</th>                    
                                <th>Abstención</th>                    
                            </tr>
                        </thead>
                        <tbody>
                            <xsl:for-each select="listavotantes/votante">
                                <tr>
                                    <td>
                                        <xsl:value-of select="departamento"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="inscritos"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="partido1"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="partido2"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="partido3"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="blanco"/>
                                    </td>
                                    <td>  
                                        <!--<xsl:variable name="totalVotos" select="((partido1 + partido2+ partido3) * 100) / inscritos"/>-->            
                                        <xsl:variable name="totalNoVotos" select="inscritos - (partido1 + partido2+ partido3)"/>                         
                                        <xsl:variable name="totalVotos" select="($totalNoVotos * 100) div inscritos"/>                           
                                        <xsl:value-of select="$totalVotos"/>                             
                                        <!--<xsl:value-of select="num($totalVotos)"/>-->
                                        <!--<xsl:value-of select="format-number($totalVotos, '##0,## %;-##0,## %','european')"/>-->
                                    </td>                                
                                </tr>
                            </xsl:for-each>
                            <tr>
                                <td colspan="2">
                                    Total
                                </td>
                                <td >
                                    <xsl:value-of select="sum(listavotantes/votante/partido1)"/>
                                </td>
                                <td >
                                    <xsl:value-of select="sum(listavotantes/votante/partido2)"/>
                                </td>
                                <td >
                                    <xsl:value-of select="sum(listavotantes/votante/partido3)"/>
                                </td>
                                <td >
                                    <xsl:value-of select="sum(listavotantes/votante/blanco)"/>
                                </td>
                                <td>
                              
                                </td>
                            </tr>
                        </tbody>
                    </table> 
                    
                </div>
      
                <br/>     
                <div>
                    <h1>3. Votantes en en la región Caribe </h1>
                </div>  
                <table border="1">     
                    <thead>
                        <tr>
                            <th>Departamentos</th>         
                            <th>Inscritos</th>
                            <th>Partido 1</th>  
                            <th>Partido 2</th>
                            <th>Partido 3</th>
                            <th>Blanco</th>                    
                            <th>Abstención</th>                    
                        </tr>
                    </thead>
                    <tbody>
                        <xsl:for-each select="listavotantes/votante">
                            <xsl:variable name="regdep" select="departamento/@region"> </xsl:variable> 
                            <xsl:if test="contains($regdep,'Caribe')"> 
                                <tr>
                                    <td>
                                        <xsl:value-of select="departamento"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="inscritos"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="partido1"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="partido2"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="partido3"/>
                                    </td>
                                    <td>
                                        <xsl:value-of select="blanco"/>
                                    </td>
                                    <td>          
                                        <xsl:variable name="totalNoVotos" select="inscritos - (partido1 + partido2+ partido3)"/>                         
                                        <xsl:variable name="totalVotos" select="($totalNoVotos * 100) div inscritos"/>                           
                                        <xsl:value-of select="$totalVotos"/>                             
                                    </td>
                                </tr>
                            </xsl:if>
                        </xsl:for-each>
                    </tbody>
                </table> 
                <br/>                 
                <form >
                    <label for="regiones">Región: </label>
                    <select name="regiones" id="regiones">
                        <!--                        <xsl:for-each-group select="listavotantes/votante" group-by="@region">
                            <option>
                                <xsl:value-of select="."/>
                            </option>
                        </xsl:for-each-group>-->
                        <xsl:for-each select="listavotantes/votante">                   
                            <xsl:variable name="regionesdep" select="departamento/@region"> </xsl:variable>     
                            <option>
                                <xsl:value-of select="$regionesdep"/>
                            </option>    
                          
                            <!--                            <xsl:template match="text()" name="split">
                                <xsl:param name="pText" select="."/>
                                <xsl:if test="string-length($pText)">
                                    <xsl:if test="not($pText=.)">
                                        <br />
                                    </xsl:if>
                                    <xsl:value-of select="substring-before(concat($pText,';'),';')"/>
                                    <xsl:call-template name="split">
                                        <xsl:with-param name="pText" select="substring-after($pText, ';')"/>
                                    </xsl:call-template>
                                </xsl:if>
                            </xsl:template>-->
                                                              
                                                                                                                                  
                        </xsl:for-each>
                        <!--                        https://stackoverflow.com/questions/50224452/error-unsupported-xsl-element-http-www-w3-org-1999-xsl-transformfor-each-g
                        -->                       
                        <!--                       
                        -->                                          
                    </select>
                    <input type="submit" value="Filtrar" />
                </form>
                <div>
                    <h1>4. Cantidad de letras de los departamentos en la región Andina </h1>
                </div>
                <ol>
                    <xsl:for-each select="listavotantes/votante">
                        <xsl:variable name="regdep" select="departamento/@region"> </xsl:variable> 
                        <xsl:if test="contains($regdep,'Andina')"> 
                            <xsl:variable name="dep" select="translate(departamento, ' ','')"> </xsl:variable>                                            
                            <li> 
                                <xsl:copy-of select="concat($dep, '-', string-length($dep))" />
                            </li>
                        </xsl:if> 
                    </xsl:for-each>
                </ol> 
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
