<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <html>
            <head>
               <script type="text/javascript">
                 <![CDATA[         
                      var el;
                    function toggleModal(Id) {
                      if (el != null && el!=document.getElementById(Id)){
                        el.style.visibility = "hidden";
                       }
	                    el = document.getElementById(Id);
	                    el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
                    }
                    function closeModal(Id) {
	                    el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
                    }
                 ]]>
              </script>
                <link media="screen" href="style.css" type="text/css" rel="stylesheet"/>
            </head>
            <body>
                <div class="main">
                    <xsl:apply-templates select="ArrayOfGamesByGenre" />
                </div>
            </body>
        </html>
    </xsl:template>
    <xsl:template match="ArrayOfGamesByGenre">
    <span class="div-span">
    <xsl:for-each select="GamesByGenre">
		<div class="genre-div"> <br/>
		<h2><xsl:value-of select="Name"/></h2> <br/><br/>
			<div class="flex-container">
				<xsl:variable name="maxAmount" select="Games/GameShort[not(Popularity &lt; ../GameShort/Popularity)]/Popularity" />
				<xsl:variable name="minValue" select="Games/GameShort[not(Popularity &gt; ../GameShort/Popularity)]/Popularity" />
				<xsl:variable name="perc100" select="$maxAmount - $minValue"/>
				<xsl:variable name="perc1">
					<xsl:choose>
						<xsl:when test="$perc100 = 0">100</xsl:when>
						<xsl:otherwise><xsl:value-of select="100 div $perc100"/></xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="maxFont">30</xsl:variable>
				<xsl:variable name="minFont">18</xsl:variable>
				<xsl:variable name="fontDiff" select="$maxFont - $minFont"/>
        <div>
          <xsl:for-each select="Games/GameShort">
            <xsl:variable name="fontSize" select="$minFont + ceiling($fontDiff div 100 * ((Popularity - $minValue) * $perc1))"/>
            <button class="genre-button" style="font-size: {$fontSize}px" onclick="toggleModal({Id})">
              <xsl:value-of select="Name"/>
            </button>
            <div class="modal-div" id="{Id}">
              <div>
                <h3>
                  <xsl:value-of select="Name"/>
                </h3> <br/>
                ID: <xsl:value-of select="Id"/> <br/>
                <button class="modal-button" onclick="closeModal({Id})">Close</button>
              </div>
            </div>
          </xsl:for-each>
        </div>
			<br/>
			</div>
		</div>
     </xsl:for-each>
      </span>
    </xsl:template>
</xsl:stylesheet>