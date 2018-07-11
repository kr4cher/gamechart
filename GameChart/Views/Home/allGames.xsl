<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <html>
            <head>
                <link media="screen" href="style.css" type="text/css" rel="stylesheet"/>
            </head>
            <body>
                <div class="main">
                    <xsl:apply-templates select="games/genre" />
                </div>
            </body>
        </html>
    </xsl:template>
    <xsl:template match="games/genre">
		<div class="{gname}-div"> <br/>
		<i><xsl:value-of select="gname"/></i> <br/><br/>
			<div class="flex-container">
				
				<xsl:variable name="maxAmount" select="game[not(popularity &lt; ../game/popularity)]/popularity" />
				<xsl:variable name="minValue" select="game[not(popularity &gt; ../game/popularity)]/popularity" />
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
				<xsl:for-each select="game">
					<xsl:variable name="fontSize" select="$minFont + ceiling($fontDiff div 100 * ((popularity - $minValue) * $perc1))"/>

					<button class="{../gname}-button" style="font-size: {$fontSize}px">
						<xsl:value-of select="title"/>
					</button>
				</xsl:for-each>
			</div>
			<br/>
			</div>
		</div>
    </xsl:template>
</xsl:stylesheet>