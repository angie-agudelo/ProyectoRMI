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
                <script type="javascript" src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js"></script>
                <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
                <script language="javascript" type="text/javascript">
                   <![CDATA[
                      $(document).ready(function() {
                      debugger;
                        $('#regiones option').each(function() {
                        var valtext = this.text;
                            if($('#regiones option[value="'+valtext+'"]').length > 1){                        
                                $(this).remove();
                            }
                        });
                            $('#regiones').on('change', function() { 
                                var valorSelect = this.value;       
                                $("#tblRegion tr").filter(function() {
                                    $(this).toggle($(this).text().toLowerCase().indexOf(valorSelect.toLowerCase()) > -1)
                               });
                             });
                        });
                    ]]>
                </script>
                <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous"/>
                <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
                <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
            </head>
            <body>
                <div id="accordion">
                    <div class="card">
                        <div class="card-header" id="headingOne">
                            <h5 class="mb-0">
                                <button class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    Información sobre las votaciones de gobernadores
                                </button>
                            </h5>
                        </div>

                        <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                            <div class="card-body">
                                <label for="regiones">Región: </label>
                                <select name="regiones" id="regiones">     
                                    <option value=""> Seleccione..
                                    </option>                   
                                    <xsl:for-each select="listavotantes/votante">                   
                                        <xsl:variable name="regionesdep" select="departamento/@region"> </xsl:variable>     
                                        <option value="{$regionesdep}">
                                            <xsl:value-of select="$regionesdep"/>
                                        </option>                                                                                                                    
                                    </xsl:for-each>                                                          
                                </select>
                                <xsl:param name="totalAbstencion">0</xsl:param>                           
                                <table class="table table-hover" id="tblRegion">  
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
                                            <!--<xsl:if test="contains($regdep,'Caribe')">--> 
                                            <tr>
                                                <td style="display:none;">
                                                    <xsl:value-of select="$regdep"/>
                                                </td>
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
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header" id="headingThree">
                            <h5 class="mb-0">
                                <button class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                    Cantidad de letras de los departamentos en la región Andina
                                </button>
                            </h5>
                        </div>
                        <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
                            <div class="card-body">
          
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
                            </div>
                        </div>
                    </div>
                </div>    
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
