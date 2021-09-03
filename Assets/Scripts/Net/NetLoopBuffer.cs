using UnityEngine;
using System.Collections;
using System;

public class NetLoopBuffer {
	
	private const int MIN_BUFF_LEN = 65535;
	private int m_BuffLen;
	private byte[] m_Buffer;
	private int m_In = 0;
	private int m_Out = 0;
	
	
	public NetLoopBuffer(int buffLen)
	{
		if(buffLen <= MIN_BUFF_LEN)
		{
			m_BuffLen = buffLen;
		}
		
		m_BuffLen = buffLen;
		m_Buffer = new byte[m_BuffLen];
	}
	
	public void Reset()
	{
		m_In = 0; 
		m_Out = 0;
	}
	
	public bool Push(byte[] arrInData, int nOffset, int nLength)
	{
		int nLeftSpace = 0;
		if (m_In < m_Out)
		{
			// 读取位置在写入位置之后
			nLeftSpace = m_Out - m_In;
			if (nLeftSpace < nLength)
			{
				Debug.Log("Buffer is full! Input length: " + nLength + ", LeftSpace: " + nLeftSpace);
				// Reset();
				return false;
			}
			
			Array.Copy(arrInData, nOffset, m_Buffer, m_In, nLength);
			
			m_In += nLength;
		}
		else
		{
			// 读取位置在写入位置之前
			nLeftSpace = m_BuffLen - (m_In - m_Out);
			if (nLeftSpace < nLength)
			{
				Debug.Log("Buffer is full! Input length: " + nLength + ", LeftSpace: " + nLeftSpace);
				// Reset();
				return false;
			}
			
			if (m_BuffLen - m_In < nLength)
			{
				// 后面放不下
				int nFirstSectionLength = m_BuffLen - m_In;
				
				// 先拷贝前一段
				Array.Copy(arrInData, nOffset, m_Buffer, m_In, nFirstSectionLength);
				
				// 再拷贝后一段
				Array.Copy(arrInData, nOffset + nFirstSectionLength, m_Buffer, 0, nLength - nFirstSectionLength);
				
				m_In = nLength - nFirstSectionLength;
			}
			else
			{
				// 后面放得下
				Array.Copy(arrInData, nOffset, m_Buffer, m_In, nLength);
				
				m_In += nLength;
			}
		}
		
		return true;
	}
	
	// 弹出nLength的数据
	public bool Pop(ref byte[] arrOutData, int nLength)
	{
		int nOccupiedSpace = 0;
		if (m_In < m_Out)
		{
			// 读取位置在写入位置之后
			nOccupiedSpace = m_BuffLen - (m_Out - m_In);
			if (nLength > nOccupiedSpace)
			{
				Debug.Log("Occupied space is little than input nLength! Pop Length: " + nLength + ", OccupiedSpace: " + nOccupiedSpace);
				// Reset();
				return false;
			}
			
			if (nLength < (m_BuffLen - m_Out))
			{
				// 所要读取的数据都在后面
				Array.Copy(m_Buffer, m_Out, arrOutData, 0, nLength);
				
				m_Out += nLength;
			}
			else
			{
				// 要读取的数据被分成了两部分
				int nFirstSectionLength = m_BuffLen - m_Out;
				// 先拷贝前一段
				Array.Copy(m_Buffer, m_Out, arrOutData, 0, nFirstSectionLength);
				
				// 再拷贝后一段
				Array.Copy(m_Buffer, 0, arrOutData, nFirstSectionLength, nLength - nFirstSectionLength);
				
				m_Out = nLength - nFirstSectionLength;
			}
		}
		else
		{
			// 读取位置在写入位置之前
			nOccupiedSpace = m_In - m_Out;
			if (nLength > nOccupiedSpace)
			{
				Debug.LogError("Occupied space is little than input nLength! Pop Length: " + nLength + ", OccupiedSpace: " + nOccupiedSpace);
				// Reset();
				return false;
			}
			
			Array.Copy(m_Buffer, m_Out, arrOutData, 0, nLength);
			
			m_Out += nLength;
		}
		
		return true;
	}
}
